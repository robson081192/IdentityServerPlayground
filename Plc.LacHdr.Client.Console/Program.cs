using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using SystemConsole = System.Console;

namespace Plc.LacHdr.Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SystemConsole.WriteLine("Hello. This is a test of the Plc.LacHdr console client");
            SystemConsole.WriteLine("To start the test press ENTER...");
            SystemConsole.ReadLine();

            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync("https://localhost:5001").Result;
            if (disco.IsError)
            {
                SystemConsole.WriteLine(disco.Error);
                return;
            }

            // request token
            var response = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "Plc.LacHdr.Api.Client",
                ClientSecret = "secret",
                Scope = "Plc.LacHdr.Api"
            }).Result;

            if (response.IsError)
            {
                SystemConsole.WriteLine(response.Error);
                return;
            }

            SystemConsole.WriteLine(response.Json);

            // call api
            var client2 = new HttpClient();
            client2.SetBearerToken(response.AccessToken);

            var apiResponse = client2.GetAsync("https://localhost:5011/api/secure").Result;
            if (!apiResponse.IsSuccessStatusCode)
            {
                SystemConsole.WriteLine(apiResponse.StatusCode);
            }
            else
            {
                var content = apiResponse.Content.ReadAsStringAsync().Result;
                SystemConsole.WriteLine(JArray.Parse(content));
            }

            SystemConsole.ReadLine();
        }
    }
}

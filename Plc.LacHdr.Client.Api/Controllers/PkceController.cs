using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace Plc.LacHdr.Api.Controllers
{
    public class PkceController : Controller
    {
        //  TODO: When you have some free time complete this "own" Authorization Code Flow implementation
        private const string AUTHORIZE_ENDPOINT = "https://localhost:5001/connect/authorize";
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            var nonce = CryptoRandom.CreateRandomKeyString(64);
            var verifier = CryptoRandom.CreateRandomKeyString(64);
            var challenge = verifier.ToSha256();

            var requestUrl = new RequestUrl(AUTHORIZE_ENDPOINT);
            var client = new HttpClient();

            var url = requestUrl.Create(new
                {
                    client_id = "Plc.LacHdr.Client.Mvc.AuthCode",
                    response_type = "code",
                    scope = "openid profile Plc.LacHdr.Api",
                    redirect_uri = "https://localhost:5021/auth-callback",
                    code_challenge = challenge,
                    code_challenge_method = "S256"
            }
            );
            var response = await client.GetAsync(url);
            return Redirect(url);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Plc.IdentityServer
{
    public static class IsConfg
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("Plc.LacHdr.Api", "Lac Help desk replacement API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Plc.LacHdr.Client.Mvc",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowOfflineAccess = true,
                    RedirectUris = { "https://localhost:5021/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5021/signout-callback-oidc" },
                    AllowedCorsOrigins =     { "https://localhost:5021" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "Plc.LacHdr.Api"
                    }
                },
                new Client
                {
                    ClientId = "Plc.LacHdr.Client.AngularCli.Implicit",
                    ClientName = "Plain Angular Cli Client (Implicit)",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = new List<string> { "http://localhost:4200/auth-callback" },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Plc.LacHdr.Api"
                    }
                },
                new Client
                {
                    ClientId = "Plc.LacHdr.Client.AngularCli.AuthCode",
                    ClientName = "Plain Angular Cli Client (Authorization Code)",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = true,
                    //RequirePkce = true,
                    RedirectUris = new List<string> { "http://localhost:4200/auth-callback" },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Plc.LacHdr.Api"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "john",
                    Password = "P@ssw0rd",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "john@doe.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    },
                }
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plc.LacHdr.Api.Models;

namespace Plc.LacHdr.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> DisplayClaims()
        {
            //  TODO: Rename this endpoint to an 'login endpoint' and implement it in the Plc.LacHdr.Api
            //  TODO: Try also to add to that project two schemas for authentication (JWT and Cookie + Identity Server)
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");
            return View(new DisplayClaimsModel
            {
                AccessToken = accessToken, RefreshToken = refreshToken
            });
        }

        [Authorize]
        public async Task<IActionResult> ReturnClaimsForAngular(string returnUrl)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            return Redirect($"{returnUrl}?accessToken={accessToken}");
        }

        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await HttpContext.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}

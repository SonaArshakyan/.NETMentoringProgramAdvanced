using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcLoginClient.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace MvcLoginClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var _idToken = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
            var _accessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

            var tokenViewModel = new TokenInfoViewModel
            {
                AccessToken = _accessToken,
                IdToken = _idToken,
                RefreshToken = refreshToken
            };
            return View(tokenViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

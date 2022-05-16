using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NSE.WebApp.MVC.Controllers
{
    public class IdentityController : MainController 
    {
        private readonly IAuthService _authService;

        public IdentityController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            var response = await _authService.Register(userRegister);
            
            if (ResponseHasErrors(response.ResponseResult)) return View(userRegister);

            await SignIn(response);

            return RedirectToAction("Index", "Home");

        }

        
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin, string? returnUrl = null )
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(userLogin);

            var response = await _authService.Login(userLogin);

            if (ResponseHasErrors(response.ResponseResult)) return View(userLogin);

            await SignIn(response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");

        }


        private async Task SignIn(UserLoginResponse response)
        {
            var token = GetHandledJwtToken(response.AcessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", response.AcessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };
            
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
        }
        

        private static JwtSecurityToken GetHandledJwtToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
        }
    }
}

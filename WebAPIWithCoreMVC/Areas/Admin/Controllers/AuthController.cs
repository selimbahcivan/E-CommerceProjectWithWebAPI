using Entities.DTOs.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPIWithCoreMVC.ApiServices.Interfaces;

namespace WebAPIWithCoreMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        IAuthApiService _authApiService;
        IHttpContextAccessor _httpContextAccessor;
        public AuthController(IAuthApiService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authApiService = authService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _authApiService.LoginAsync(loginDto);
            if (user.Success)
            {
                _httpContextAccessor.HttpContext.Session.SetString("token", user.Data.Token);
                var userClaims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                userClaims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()));
                userClaims.AddClaim(new Claim(ClaimTypes.Name, user.Data.UserName));
                var claimPrincipal = new ClaimsPrincipal(userClaims);
                var authProperties = new AuthenticationProperties() { IsPersistent = loginDto.IsRememberMe };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, authProperties);
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", "Username or Password is Incorrect !");
            }
            return View(loginDto);
        }
    }
}

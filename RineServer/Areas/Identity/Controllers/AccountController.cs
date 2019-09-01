using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RineServer.Areas.Identity.Models;
using RineServer.Areas.Identity.ControllerModels;

namespace RineServer.Areas.Identity.Controllers
{
    [Area("Identity")]
    [ApiController]
    public class AccountController : Controller
    {
        private static readonly SigningCredentials SigningCreds = new SigningCredentials(RineServer.Startup.SecurityKey, SecurityAlgorithms.HmacSha256);

        private readonly SignInManager<RineUser> _signInManager;
        private readonly UserManager<RineUser> _userManager;
        private readonly ILogger _logger;
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        public AccountController(
            SignInManager<RineUser> signInManager,
            UserManager<RineUser> userManager,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme);
        }

        [HttpPost]
        public async Task<IActionResult> Token(UserLogin userInfo)
        {
            var user = await _userManager.FindByNameAsync(userInfo.UserName);
            if (user == null)
            {
                return Json(new
                {
                    code = 1,
                    messages = new List<string> { "no such user" },
                });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userInfo.Password, false);
            if (result.Succeeded)
            {
                var principle = await _signInManager.CreateUserPrincipalAsync(user);
                var token = new JwtSecurityToken(
                    "Rine",
                    "Rine",
                    principle.Claims,
                    expires: DateTime.UtcNow.AddDays(30),
                    signingCredentials: SigningCreds);
                return Json(new
                {
                    code = 0,
                    token = _tokenHandler.WriteToken(token),
                });
            }
            else
            {
                return Json(new
                {
                    code = 2,
                    messages = new List<string> { "Login failed" },
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegister userInfo)
        {
            var user = new RineUser { UserName = userInfo.UserName };
            var result = await _userManager.CreateAsync(user, userInfo.Password);
            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, userInfo.UserName));

                _logger.LogInformation($"User created a new account with UserName: {userInfo.UserName}");
                return Json(new
                {
                    code = 0,
                    messages = new List<string> { "registration success" },
                });
            }
            else
            {
                return Json(new
                {
                    code = 1,
                    messages = result.Errors.ToList(),
                });
            }
        }


    }
}
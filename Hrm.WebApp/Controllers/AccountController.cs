using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Hrm.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ISettingsService _settingsService;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(
            SignInManager<User> signInManager,
            ISettingsService settingsService,
            IAccountService accountService,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _settingsService = settingsService;
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {

            if (string.IsNullOrEmpty(_configuration.GetConnectionString("DefaultConnection")))
            {
                return RedirectToAction("Index", "Setup");
            }

            //ViewData["Title"] = await _settingsService.GetOrganizationName();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel, string? returnURL = null)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            TempData["OrganizationName"] = await _settingsService.GetOrganizationName();

            var signinResult = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);

            if (signinResult.Succeeded)
            {
                return string.IsNullOrEmpty(returnURL) ?
                    RedirectToAction("Index", "Dashboard") :
                    LocalRedirect(returnURL);
            }

            ModelState.AddModelError("Error", "Invalid email or password");
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var profile = await _accountService.GetProfileAsync(userId);
            return View(profile);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}

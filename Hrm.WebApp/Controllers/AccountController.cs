using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ISettingsService _settingsService;

        public AccountController(
            SignInManager<User> signInManager,
            ISettingsService settingsService)
        {
            _signInManager = signInManager;
            _settingsService = settingsService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            ViewData["Title"] = await _settingsService.GetOrganizationName();
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

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}

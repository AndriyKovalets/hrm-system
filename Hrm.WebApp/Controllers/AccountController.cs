using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.Exeptions;
using Hrm.Domain.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Hrm.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IAccountService accountService,
            SignInManager<User> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
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
    }
}

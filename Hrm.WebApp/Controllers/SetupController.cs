using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Exeptions;
using Hrm.Domain.Models.Setup;
using Hrm.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hrm.WebApp.Controllers
{
    public class SetupController : Controller
    {
        private readonly ISetupService _setupService;

        public SetupController(ISetupService setupService)
        {
            _setupService = setupService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SetupOrganizationModel setupOrganization)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", setupOrganization);
            }

            try
            {
                await _setupService.SetupOrganizationAsync(setupOrganization);
            }
            catch (BadConnectionStringExeption ex)
            {
                ModelState.AddModelError(nameof(SetupOrganizationModel.ConnectionString), ex.Message);
                return View("Index", setupOrganization);
            }
            catch(UserCreateExeption ex)
            {
                ModelState.AddModelError(nameof(SetupOrganizationModel.Password), ex.Message);
                return View("Index", setupOrganization);
            }

            return RedirectToAction("Index", "Home");


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

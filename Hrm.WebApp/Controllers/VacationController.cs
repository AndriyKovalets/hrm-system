using Hrm.Application.Abstractions.Services;
using Hrm.Domain.ViewModels.Vacation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hrm.WebApp.Controllers
{
    [Authorize]
    public class VacationController : Controller
    {
        private readonly IVacationService _vacationService;

        public VacationController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }

        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vacationFulInfo = await _vacationService.GetVacationFullInfoAsync(UserId);
            return View(vacationFulInfo);
        }

        [HttpGet]
        public IActionResult VacationRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VacationRequest(VacationRequesModel vacationRequest)
        {
            vacationRequest.UserId = UserId;
            await _vacationService.AddVacationRequest(vacationRequest);

            return RedirectToAction("Index");
        }
    }
}

﻿using Hrm.Application.Abstractions.Services;
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
        private readonly IVacationPlanService _vacationPlanService;

        public VacationController(IVacationService vacationService, IVacationPlanService vacationPlanService)
        {
            _vacationService = vacationService;
            _vacationPlanService = vacationPlanService;
        }

        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vacationFulInfo = await _vacationService.GetVacationFullInfoAsync(UserId);
            vacationFulInfo.VacationRates = await _vacationService.GetVacationRatesAsync(UserId);

            return View(vacationFulInfo);
        }

        [HttpGet]
        public  async Task<IActionResult> VacationRequest()
        {
            ViewBag.Plan = await _vacationService.GetVacationPlanForUserAsync(UserId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VacationRequest(VacationRequesModel vacationRequest)
        {
            vacationRequest.UserId = UserId;
            await _vacationService.AddVacationRequestAsync(vacationRequest);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Management()
        {
            var notAcceptedVacancy = await  _vacationService.GetNotAcceptVacationAsync();
            return View(notAcceptedVacancy);
        }

        [HttpGet]
        public async Task<IActionResult> Accept(int vacationId, bool isAccept)
        {
            await _vacationService.AcceptVacationAsync(vacationId, isAccept);
            return RedirectToAction("Management");
        }

        [HttpGet]
        public async Task<IActionResult> History(string userId)
        {
            var vacationFulInfo = await _vacationService.GetVacationFullInfoAsync(userId);
            return View(vacationFulInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Calendar()
        {
            var curentVacationList = await _vacationService.GetCurrentVacationAsync();

            return View(curentVacationList);
        }

        [HttpGet]
        public IActionResult AddRate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRate(VacationRateModel model)
        {
            await _vacationService.AddVacationRateAsync(model, UserId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRate(int id)
        {
            await _vacationService.DeleteVacationRatesAsync(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Plan()
        {
            var planList = await _vacationService.GetVacationPlanAsync();
            return View(planList);
        }

        [HttpGet]
        public async Task<IActionResult> GeneratePlan()
        {
            await _vacationPlanService.CreateVacationPlanAsync();

            return View("Plan");
        }
    }
}

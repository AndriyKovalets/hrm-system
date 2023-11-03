using Hrm.Application.Abstractions.Services;
using Hrm.Domain.ViewModels.Dashboard;
using Hrm.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hrm.WebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly INewService _newService;
        private readonly IVacationService _vacationService;

        public DashboardController(IConfiguration configuration, INewService newService, IVacationService vacationService)
        {
            _configuration = configuration;
            _newService = newService;
            _vacationService = vacationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(_configuration.GetConnectionString("DefaultConnection")))
            {
                return RedirectToAction("Index", "Setup");
            }

            var lastNews = await _newService.GetNewsListAsync(3);
            var todayInVacation = await _vacationService.TodayInVacationAsync();
            var dashboardModel = new DashboardModel()
            {
                LastNews = lastNews,
                TodayInVacation = todayInVacation
            };

            return View(dashboardModel);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

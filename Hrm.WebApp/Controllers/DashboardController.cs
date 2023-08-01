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

        public DashboardController(IConfiguration configuration, INewService newService)
        {
            _configuration = configuration;
            _newService = newService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(_configuration.GetConnectionString("DefaultConnection")))
            {
                return RedirectToAction("Index", "Setup");
            }

            var lastNews = await _newService.GetNewsListAsync(3);

            var dashboardModel = new DashboardModel()
            {
                LastNews = lastNews
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

using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.New;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hrm.WebApp.Controllers
{
    public class NewController : Controller
    {
        private readonly INewService _newService;

        public NewController(INewService newService)
        {
            _newService = newService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var news = await _newService.GetNewsListAsync();

            return View(news);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var news = await _newService.GetNewsAsync(id.Value);

            return View(news);
        }

        [HttpGet]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id.HasValue)
            {
                var news = await _newService.GetNewsAsync(id.Value);
                return View(news);
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public async Task<IActionResult> Edit(NewShortInfoModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            string? userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(model.Id == 0)
            {
                await _newService.AddNewAsync(model, userId);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}

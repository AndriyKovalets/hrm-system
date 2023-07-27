using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.New;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public IActionResult Edit(NewEditModel model)
        {
            return View();
        }
    }
}

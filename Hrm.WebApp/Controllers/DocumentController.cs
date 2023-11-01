using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Document;
using Hrm.Domain.ViewModels.New;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hrm.WebApp.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var documents = await _documentService.GetDodumentListAsync();

            return View(documents);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var document = await _documentService.GetDodumentAsync(id.Value);

            return View(document);
        }

        [HttpGet]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                var document = await _documentService.GetDodumentAsync(id.Value);
                return View(document);
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
        public async Task<IActionResult> Edit(DocumentShortInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (model.Id == 0)
            {
                await _documentService.AddDodumentAsync(model, userId);
                return RedirectToAction("Index");
            }
            else
            {
                await _documentService.EditDocumentAsync(model, userId);
                return RedirectToAction("Details", new { id = model.Id });
            }

        }
    }
}

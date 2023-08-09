using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.WebApp.Controllers
{
    [Authorize(Roles = $"{SystemRoles.Admin}, {SystemRoles.Manager}")]
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<IActionResult> Index()
        {
            var periodList = _settingsService.GetPeriodList();

            var vaccinationSettings = await _settingsService.GetVacationSettingsAsync() ?? new VacationSettingsModel();
            vaccinationSettings.PeriodSelectList = periodList;

            var settingModel = new SettingsModel();
            settingModel.VaccinationSettings = vaccinationSettings;

            return View(settingModel);
        }

        public async Task<IActionResult> Edit(SettingsModel model)
        {
            await _settingsService.EditVaccinationSettings(model.VaccinationSettings!);

            return RedirectToAction("Index");
        }
    }
}

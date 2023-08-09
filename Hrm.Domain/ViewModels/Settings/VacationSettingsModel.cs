using Hrm.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrm.Domain.ViewModels.Settings
{
    public class VacationSettingsModel: VacationSettings
    {
        public IEnumerable<SelectListItem>? PeriodSelectList { get; set; }
    }
}

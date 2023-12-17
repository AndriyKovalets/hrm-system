using Hrm.Domain.Models;

namespace Hrm.Domain.ViewModels.Settings
{
    public class SettingsModel
    {
        public VacationSettingsModel? VaccinationSettings { get; set; }
        public VacationPlanSettings? VacationPlanSettings { get; set; }
    }
}

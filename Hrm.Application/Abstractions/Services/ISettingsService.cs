using Hrm.Domain.Models;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrm.Application.Abstractions.Services
{
    public interface ISettingsService
    {
        Task<string?> GetOrganizationName();
        Task<VacationSettingsModel?> GetVacationSettingsAsync();
        Task EditVacationSettings(VacationSettings settings);
        IEnumerable<SelectListItem> GetPeriodList(DepartmentRolesEnum? selectDepartmenRole = null);
        Task<VacationPlanSettings?> GetVacationPlanSettingsAsync();
        Task EditVacationPlanSettings(VacationPlanSettings settings);
    }
}

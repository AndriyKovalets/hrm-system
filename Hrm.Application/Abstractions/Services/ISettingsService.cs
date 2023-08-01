using Hrm.Domain.Models;

namespace Hrm.Application.Abstractions.Services
{
    public interface ISettingsService
    {
        Task<string?> GetOrganizationName();
        Task<VaccinationSettings?> GetVaccinationSettings();
        Task EditVaccinationSettings(VaccinationSettings settings);
    }
}

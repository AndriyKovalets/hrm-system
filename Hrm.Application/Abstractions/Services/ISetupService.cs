using Hrm.Domain.Models.Setup;

namespace Hrm.Application.Abstractions.Services
{
    public interface ISetupService
    {
        Task SetupOrganizationAsync(SetupOrganizationModel setupOrganization);
    }
}

using Hrm.Domain.ViewModels.Setup;

namespace Hrm.Application.Abstractions.Services
{
    public interface ISetupService
    {
        Task SetupOrganizationAsync(SetupOrganizationModel setupOrganization);
    }
}

namespace Hrm.Application.Abstractions.Services
{
    public interface IOrganizationSettingsService
    {
        Task<string?> GetOrganizationNameAsync();
    }
}

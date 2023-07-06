namespace Hrm.Application.Abstractions.Services
{
    public interface ISettingsService
    {
        Task<string?> GetOrganizationName();
    }
}

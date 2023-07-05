using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Application.OrganizationSettings;
using Hrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Services
{
    internal class OrganizationSettingsService: IOrganizationSettingsService
    {
        private readonly IRepository<OrganizationSetting> _organizationSettingsRepository;

        private string? _organizationName;

        public OrganizationSettingsService(IRepository<OrganizationSetting> organizationSettingsRepository)
        {
            _organizationSettingsRepository = organizationSettingsRepository;
        }

        public async Task<string?> GetOrganizationNameAsync()
        {
            if (!string.IsNullOrEmpty(_organizationName))
            {
                return _organizationName;
            }

            var result = await _organizationSettingsRepository
                .Query()
                .FirstOrDefaultAsync(x => x.Name == OrganizationSettingsName.OrganozationName);
            _organizationName = result?.Settings;

            return _organizationName;
        }

    }
}

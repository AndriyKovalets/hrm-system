using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Application.OrganizationSettings;
using Hrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Services
{
    internal class SettingsService : ISettingsService
    {
        private readonly IRepository<OrganizationSetting> _settingsRepository;

        public SettingsService(IRepository<OrganizationSetting> settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }
        public async Task<string?> GetOrganizationName()
        {
            var result = await _settingsRepository
                .Query()
                .FirstOrDefaultAsync(x => x.Name == OrganizationSettingsName.OrganozationName);

            return result?.Settings;
        }
    }
}

using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Application.OrganizationSettings;
using Hrm.Domain.Entities;
using Hrm.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

        public async Task<VaccinationSettings?> GetVaccinationSettings()
        {
            var result = await _settingsRepository
                .Query()
                .FirstOrDefaultAsync(x => x.Name == OrganizationSettingsName.VaccinationSettings);

            return JsonSerializer.Deserialize<VaccinationSettings>(result?.Settings);
        }

        public async Task EditVaccinationSettings(VaccinationSettings settings)
        {
            var settingsFromDb = await _settingsRepository
                .Query()
                .FirstOrDefaultAsync(x => x.Name == OrganizationSettingsName.VaccinationSettings);

            if(settingsFromDb is null)
            {
                await _settingsRepository.AddAsync(new OrganizationSetting()
                {
                    Name = OrganizationSettingsName.VaccinationSettings,
                    Settings = JsonSerializer.Serialize(settings)
                });
            }
            else
            {
                settingsFromDb.Settings = JsonSerializer.Serialize(settings);
                await _settingsRepository.UpdateAsync(settingsFromDb);
            }

            await _settingsRepository.SaveChangesAsync();
        }
    }
}

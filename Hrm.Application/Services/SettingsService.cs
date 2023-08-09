using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Application.OrganizationSettings;
using Hrm.Domain.Entities;
using Hrm.Domain.Enums;
using Hrm.Domain.Models;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<VacationSettingsModel?> GetVacationSettingsAsync()
        {
            var result = await _settingsRepository
                .Query()
                .FirstOrDefaultAsync(x => x.Name == OrganizationSettingsName.VacationSettings);

            return JsonSerializer.Deserialize<VacationSettingsModel>(result?.Settings ?? "{}");
        }

        public async Task EditVaccinationSettings(VacationSettings settings)
        {
            var settingsFromDb = await _settingsRepository
                .Query()
                .FirstOrDefaultAsync(x => x.Name == OrganizationSettingsName.VacationSettings);

            if(settingsFromDb is null)
            {
                await _settingsRepository.AddAsync(new OrganizationSetting()
                {
                    Name = OrganizationSettingsName.VacationSettings,
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

        public IEnumerable<SelectListItem> GetPeriodList(DepartmentRolesEnum? selectDepartmenRole = null)
        {
            var enumValues = Enum.GetValues(typeof(VacationPeriod)).Cast<VacationPeriod>();
            var selectListItems = enumValues.Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)Convert.ChangeType(v, typeof(int))).ToString(),
                Disabled = false,
                Selected = false
            }).ToList();


            selectListItems.Insert(0, new SelectListItem
            {
                Text = "Period",
                Value = "",
                Disabled = true,
                Selected = false
            });

            if (selectDepartmenRole is not null)
            {
                var item = selectListItems.FirstOrDefault(x => x.Text == selectDepartmenRole.Value.ToString());

                item.Selected = true;
            }

            return selectListItems;
        }
    }
}

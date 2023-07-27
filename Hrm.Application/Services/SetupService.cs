using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Application.OrganizationSettings;
using Hrm.Domain.Entities;
using Hrm.Domain.Exeptions;
using Hrm.Domain.Roles;
using Hrm.Domain.ViewModels.Employee;
using Hrm.Domain.ViewModels.Setup;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Hrm.Application.Services
{
    internal class SetupService : ISetupService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<OrganizationSetting> _organizationSettingsRepository;
        private readonly IEmployeeService _userService;

        public SetupService(
            IConfiguration configuration,
            IRepository<OrganizationSetting> repository,
            IEmployeeService userService)
        {
            _configuration = configuration;
            _organizationSettingsRepository = repository;
            _userService = userService;
        }

        public async Task SetupOrganizationAsync(SetupOrganizationModel setupOrganization)
        {          
            var isChange = await _organizationSettingsRepository.ChangeDbConnectionString(setupOrganization.ConnectionString!);

            if (!isChange)
            {
                throw new BadConnectionStringExeption();
            }

            await ChangeAppSettingConnectionStringAsync(setupOrganization.ConnectionString!);

            await _organizationSettingsRepository.CreateDatabase();

            await _organizationSettingsRepository.AddAsync(
                new OrganizationSetting()
                {
                    Name = OrganizationSettingsName.OrganozationName,
                    Settings = setupOrganization.OrganizationName
                }
             );

            await _organizationSettingsRepository.SaveChangesAsync();

            var adminUser = new EmployeeModel()
            {
                FirstName = setupOrganization.FirstName,
                LastName = setupOrganization.LastName,
                Email = setupOrganization.Email
            };

            await _userService.CreateEmployeeAsync(adminUser, SystemRoles.Admin, null, setupOrganization.Password);
        }

        private async Task ChangeAppSettingConnectionStringAsync(string connectionString)
        {
            var key = "ConnectionStrings:DefaultConnection";
            var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            string json = await File.ReadAllTextAsync(appSettingsPath) ?? String.Empty;

            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            var sectionPath = key.Split(":")[0];

            if (!string.IsNullOrEmpty(sectionPath))
            {
                var keyPath = key.Split(":")[1];
                jsonObj[sectionPath][keyPath] = connectionString;
            }
            else
            {
                jsonObj[sectionPath] = connectionString;
            }

            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            await File.WriteAllTextAsync(appSettingsPath, output);

        }
    }
}

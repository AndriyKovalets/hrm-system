using AutoMapper;
using Hrm.Application.Abstractions.Services;
using Hrm.Application.Helpers;
using Hrm.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrm.Application
{
    public static class StartupSetup
    {
        public static void SetupApplication(this IServiceCollection services, IConfiguration conficuration)
        {
            services.AddServices();
            services.AddAutoMapper();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISetupService, SetupService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IStorageService, LocaleStorageService>();
            services.AddScoped<INewService, NewService>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
using Hrm.Application.Abstractions.Services;
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
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISetupService, SetupService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
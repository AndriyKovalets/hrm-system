using Hrm.Domain.Roles;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Hrm.WebApp.ServiceExtentions
{
    public static class StartupSetup
    {
        public static void SetupWebApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddAuthorization();
        }

        private static void AddControllers(this IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        private static void AddAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Forbidden";
                });
        }

        private static void AddAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(SystemRoles.Admin,
                     policy => policy.RequireRole(SystemRoles.Admin));

                options.AddPolicy(SystemRoles.Manager,
                     policy => policy.RequireRole(SystemRoles.Manager));

                options.AddPolicy(SystemRoles.Member,
                     policy => policy.RequireRole(SystemRoles.Member));
            });
        }
    }
}

using Microsoft.AspNetCore.Authentication.Cookies;

namespace Hrm.WebApp.ServiceExtentions
{
    public static class StartupSetup
    {
        public static void SetupWebApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            //services.AddAuthentication();
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
    }
}

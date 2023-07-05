using Hrm.Application.Abstractions.Repositories;
using Hrm.Domain.Entities;
using Hrm.Infrastructure.Data;
using Hrm.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrm.Infrastructure
{
    public static class StartupSetup
    {
        public static void SetupInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddIdentityDbContext();
            services.AddRepositories();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services
                .AddDbContext<HrmDbContext>(x => x.UseSqlServer(connectionString));           
        }

        public static void AddIdentityDbContext(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<HrmDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        }
    }
}
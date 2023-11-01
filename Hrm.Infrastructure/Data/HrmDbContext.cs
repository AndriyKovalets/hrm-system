using Hrm.Domain.Entities;
using Hrm.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Infrastructure.Data
{
    internal class HrmDbContext: IdentityDbContext<User>
    {
        public HrmDbContext(DbContextOptions<HrmDbContext> options) :base(options)
        {
        
        }

        public DbSet<OrganizationSetting> OrganizationSettings { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<VacationHistory> VacationHistories { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<VacationPlan> VacantionPlans { get; set; }
        public DbSet<VacationRate> VacationRates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
            builder.ApplyConfiguration(new OrganizationSettingConfiguration());
            builder.ApplyConfiguration(new VacationHistoryConfiguration());
            builder.ApplyConfiguration(new VacationRateConfiguration());
            builder.ApplyConfiguration(new VacationPlanConfiguration());
            builder.ApplyConfiguration(new DocumentConfiguration());
        }
    }
}

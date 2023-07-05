using Hrm.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Infrastructure.Data
{
    internal class HrmDbContext: IdentityDbContext<User>
    {
        public HrmDbContext(DbContextOptions<HrmDbContext> options) :base(options)
        {
        
        }

        public DbSet<OrganizationSetting> OrganizationSettings { get; set; }
    }
}

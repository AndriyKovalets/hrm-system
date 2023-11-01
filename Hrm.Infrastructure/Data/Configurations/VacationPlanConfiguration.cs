using Hrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Infrastructure.Data.Configurations
{
    internal class VacationPlanConfiguration : IEntityTypeConfiguration<VacationPlan>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VacationPlan> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.VacationPlans)
                .HasForeignKey(x => x.UserId);
        }
    }
}

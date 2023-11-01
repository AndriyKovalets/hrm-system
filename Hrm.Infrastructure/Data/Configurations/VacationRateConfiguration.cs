using Hrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hrm.Infrastructure.Data.Configurations
{
    internal class VacationRateConfiguration : IEntityTypeConfiguration<VacationRate>
    {
        public void Configure(EntityTypeBuilder<VacationRate> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.VacationRates)
                .HasForeignKey(x => x.UserId);
        }
    }
}

using Hrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hrm.Infrastructure.Data.Configurations
{
    internal class VacationHistoryConfiguration : IEntityTypeConfiguration<VacationHistory>
    {
        public void Configure(EntityTypeBuilder<VacationHistory> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder
                .Property(x => x.Type)
                .IsRequired();

            builder
                .Property(x => x.Balance)
                .IsRequired();

            builder
                .Property(x => x.UserId)
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.VacationHistories)
                .HasForeignKey(x => x.UserId);
        }
    }
}

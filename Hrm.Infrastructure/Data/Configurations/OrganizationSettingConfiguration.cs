using Hrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hrm.Infrastructure.Data.Configurations
{
    internal class OrganizationSettingConfiguration : IEntityTypeConfiguration<OrganizationSetting>
    {
        public void Configure(EntityTypeBuilder<OrganizationSetting> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}

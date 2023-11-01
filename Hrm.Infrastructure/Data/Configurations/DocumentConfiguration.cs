using Hrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hrm.Infrastructure.Data.Configurations
{
    internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Header)
                .IsRequired()
                .HasMaxLength(300);

            builder
                .Property(x => x.Content)
                .IsRequired();

            builder
                .HasOne(x => x.Creator)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.CreatorId);
        }
    }
}

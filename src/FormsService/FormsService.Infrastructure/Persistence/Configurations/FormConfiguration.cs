using FormsService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FormsService.Infrastructure.Persistence.Configurations;

public class FormConfiguration : IEntityTypeConfiguration<Form>
{
    public void Configure(EntityTypeBuilder<Form> builder)
    {
        builder.ToTable("Forms");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedNever();

        builder.Property(f => f.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(f => f.EntityId)
            .HasMaxLength(50);

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.Description)
            .HasMaxLength(500);

        builder.Property(f => f.Version)
            .IsRequired();

        builder.Property(f => f.State)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(f => f.Sections)
            .HasColumnType("jsonb")
            .HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<List<Section>>(x)
                );
    }
}

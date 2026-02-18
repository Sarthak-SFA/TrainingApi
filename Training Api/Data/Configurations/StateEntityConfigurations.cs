using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Training_Api.Data.Configurations;

public sealed class StateEntityConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        RelationalEntityTypeBuilderExtensions.ToTable((EntityTypeBuilder)builder, "State");

        builder.HasKey(s => s.Id);
        builder
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(2);
    }
}
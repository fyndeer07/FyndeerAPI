using FyndeerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(Tables.Categories);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Slug)
            .HasColumnName(Columns.Categories.Slug)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(c => c.Slug)
            .HasDatabaseName(Indexes.Categories.SlugUnique)
            .IsUnique();

        builder.Property(c => c.Name)
            .HasColumnName(Columns.Categories.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}

using FyndeerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable(Tables.Reviews);

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName(Columns.General.Id)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(r => r.ProfessionalId)
            .HasColumnName(Columns.Reviews.ProfessionalId)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(r => r.ProfessionalId)
            .HasDatabaseName(Indexes.Reviews.ProfessionalId);

        builder.Property(r => r.ReviewerName)
            .HasColumnName(Columns.Reviews.ReviewerName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(r => r.IsVerified)
            .HasColumnName(Columns.Reviews.IsVerified)
            .IsRequired();

        builder.Property(r => r.Rating)
            .HasColumnName(Columns.Reviews.Rating)
            .IsRequired();

        builder.Property(r => r.Comment)
            .HasColumnName(Columns.Reviews.Comment)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(r => r.CreatedAt)
            .HasColumnName(Columns.General.CreatedAt)
            .IsRequired();
    }
}

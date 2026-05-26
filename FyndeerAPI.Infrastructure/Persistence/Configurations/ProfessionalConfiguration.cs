using System.Text.Json;
using FyndeerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Persistence.Configurations;

public class ProfessionalConfiguration : IEntityTypeConfiguration<Professional>
{
    private static readonly JsonSerializerOptions _jsonOptions = new();

    private static readonly ValueComparer<List<string>> _listComparer = new(
        (a, b) => a!.SequenceEqual(b!),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        c => c.ToList());

    public void Configure(EntityTypeBuilder<Professional> builder)
    {
        builder.ToTable(Tables.Professionals);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName(Columns.General.Id)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Slug)
            .HasColumnName(Columns.Professionals.Slug)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(p => p.Slug)
            .HasDatabaseName(Indexes.Professionals.SlugUnique)
            .IsUnique();

        builder.Property(p => p.FullName)
            .HasColumnName(Columns.Professionals.FullName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Title)
            .HasColumnName(Columns.Professionals.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.CategoryId)
            .HasColumnName(Columns.Professionals.CategoryId)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(p => p.CategoryId)
            .HasDatabaseName(Indexes.Professionals.CategoryId);

        builder.Property(p => p.CategoryName)
            .HasColumnName(Columns.Professionals.CategoryName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.YearsOfExperience)
            .HasColumnName(Columns.Professionals.YearsOfExperience)
            .IsRequired();

        builder.Property(p => p.LicenseNumber)
            .HasColumnName(Columns.Professionals.LicenseNumber)
            .HasMaxLength(100);

        builder.Property(p => p.Languages)
            .HasColumnName(Columns.Professionals.Languages)
            .HasConversion(
                v => JsonSerializer.Serialize(v, _jsonOptions),
                v => JsonSerializer.Deserialize<List<string>>(v, _jsonOptions) ?? new List<string>())
            .HasColumnType("nvarchar(max)")
            .Metadata.SetValueComparer(_listComparer);

        builder.Property(p => p.Specialties)
            .HasColumnName(Columns.Professionals.Specialties)
            .HasConversion(
                v => JsonSerializer.Serialize(v, _jsonOptions),
                v => JsonSerializer.Deserialize<List<string>>(v, _jsonOptions) ?? new List<string>())
            .HasColumnType("nvarchar(max)")
            .Metadata.SetValueComparer(_listComparer);

        builder.Property(p => p.Phone)
            .HasColumnName(Columns.Professionals.Phone)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasColumnName(Columns.Professionals.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(p => p.Email)
            .HasDatabaseName(Indexes.Professionals.EmailUnique)
            .IsUnique();

        builder.Property(p => p.Website)
            .HasColumnName(Columns.Professionals.Website)
            .HasMaxLength(500);

        builder.Property(p => p.ServiceArea)
            .HasColumnName(Columns.Professionals.ServiceArea)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(p => p.Bio)
            .HasColumnName(Columns.Professionals.Bio)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(p => p.PhotoUrl)
            .HasColumnName(Columns.Professionals.PhotoUrl)
            .HasMaxLength(500);

        builder.Property(p => p.IsSponsored)
            .HasColumnName(Columns.Professionals.IsSponsored)
            .IsRequired();

        builder.Property(p => p.IsVerified)
            .HasColumnName(Columns.Professionals.IsVerified)
            .IsRequired();

        builder.Property(p => p.Rating)
            .HasColumnName(Columns.Professionals.Rating)
            .IsRequired();

        builder.Property(p => p.ReviewCount)
            .HasColumnName(Columns.Professionals.ReviewCount)
            .IsRequired();

        builder.Property(p => p.LicenseState)
            .HasColumnName(Columns.Professionals.LicenseState)
            .HasMaxLength(100);

        builder.Property(p => p.Brokerage)
            .HasColumnName(Columns.Professionals.Brokerage)
            .HasMaxLength(200);

        builder.Property(p => p.IsAcceptingClients)
            .HasColumnName(Columns.Professionals.IsAcceptingClients)
            .IsRequired();

        builder.Property(p => p.WorkingHours)
            .HasColumnName(Columns.Professionals.WorkingHours)
            .HasMaxLength(100);

        builder.HasMany(p => p.Reviews)
            .WithOne()
            .HasForeignKey(r => r.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.TrackRecord)
            .WithOne()
            .HasForeignKey<TrackRecord>(t => t.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using System.Text.Json;
using FyndeerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Persistence.Configurations;

public class TrackRecordConfiguration : IEntityTypeConfiguration<TrackRecord>
{
    private static readonly JsonSerializerOptions _jsonOptions = new();

    private static readonly ValueComparer<List<string>> _listComparer = new(
        (a, b) => a!.SequenceEqual(b!),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        c => c.ToList());

    public void Configure(EntityTypeBuilder<TrackRecord> builder)
    {
        builder.ToTable(Tables.ProfessionalTrackRecords);

        builder.HasKey(t => t.ProfessionalId);

        builder.Property(t => t.ProfessionalId)
            .HasColumnName(Columns.ProfessionalTrackRecords.ProfessionalId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.HomesSold)
            .HasColumnName(Columns.ProfessionalTrackRecords.HomesSold)
            .IsRequired();

        builder.Property(t => t.AvgDaysOnMarket)
            .HasColumnName(Columns.ProfessionalTrackRecords.AvgDaysOnMarket)
            .IsRequired();

        builder.Property(t => t.SaleToListRatio)
            .HasColumnName(Columns.ProfessionalTrackRecords.SaleToListRatio)
            .IsRequired();

        builder.Property(t => t.PriceRangeMin)
            .HasColumnName(Columns.ProfessionalTrackRecords.PriceRangeMin)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(t => t.PriceRangeMax)
            .HasColumnName(Columns.ProfessionalTrackRecords.PriceRangeMax)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(t => t.PrimaryCounties)
            .HasColumnName(Columns.ProfessionalTrackRecords.PrimaryCounties)
            .HasConversion(
                v => JsonSerializer.Serialize(v, _jsonOptions),
                v => JsonSerializer.Deserialize<List<string>>(v, _jsonOptions) ?? new List<string>())
            .HasColumnType("nvarchar(max)")
            .Metadata.SetValueComparer(_listComparer);

        builder.Property(t => t.CommunityClients)
            .HasColumnName(Columns.ProfessionalTrackRecords.CommunityClients)
            .HasMaxLength(200)
            .IsRequired();
    }
}

using FluentMigrator;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Migrations;

[Migration(202604260004, "Create ProfessionalTrackRecords table")]
public class Migration202604260004_CreateProfessionalTrackRecordsTable : Migration
{
    public override void Up()
    {
        Create.Table(Tables.ProfessionalTrackRecords)
            .WithColumn(Columns.ProfessionalTrackRecords.ProfessionalId).AsString(50).PrimaryKey().NotNullable()
            .WithColumn(Columns.ProfessionalTrackRecords.HomesSold).AsInt32().NotNullable()
            .WithColumn(Columns.ProfessionalTrackRecords.AvgDaysOnMarket).AsInt32().NotNullable()
            .WithColumn(Columns.ProfessionalTrackRecords.SaleToListRatio).AsDouble().NotNullable()
            .WithColumn(Columns.ProfessionalTrackRecords.PriceRangeMin).AsDecimal(18, 2).NotNullable()
            .WithColumn(Columns.ProfessionalTrackRecords.PriceRangeMax).AsDecimal(18, 2).NotNullable()
            .WithColumn(Columns.ProfessionalTrackRecords.PrimaryCounties).AsString(int.MaxValue).NotNullable()
            .WithColumn(Columns.ProfessionalTrackRecords.CommunityClients).AsString(200).NotNullable();

        Create.ForeignKey($"FK_{Tables.ProfessionalTrackRecords}_{Tables.Professionals}")
            .FromTable(Tables.ProfessionalTrackRecords).ForeignColumn(Columns.ProfessionalTrackRecords.ProfessionalId)
            .ToTable(Tables.Professionals).PrimaryColumn(Columns.General.Id)
            .OnDelete(System.Data.Rule.Cascade);
    }

    public override void Down()
    {
        Delete.Table(Tables.ProfessionalTrackRecords);
    }
}

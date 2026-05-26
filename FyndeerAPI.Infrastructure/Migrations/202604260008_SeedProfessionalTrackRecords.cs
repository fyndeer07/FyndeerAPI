using FluentMigrator;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Migrations;

[Migration(202604260008, "Seed Professional Track Records")]
public class Migration202604260008_SeedProfessionalTrackRecords : Migration
{
    public override void Up()
    {
        // Bikas Shrestha (Id: 5)
        Insert.IntoTable(Tables.ProfessionalTrackRecords).Row(new
        {
            ProfessionalId = "5",
            HomesSold = 107,
            AvgDaysOnMarket = 11,
            SaleToListRatio = 1.024,
            PriceRangeMin = 380_000m,
            PriceRangeMax = 1_100_000m,
            PrimaryCounties = """["Fairfax","Prince William"]""",
            CommunityClients = "Nepali & English-speaking"
        });

        // Nisha Lama (Id: 6)
        Insert.IntoTable(Tables.ProfessionalTrackRecords).Row(new
        {
            ProfessionalId = "6",
            HomesSold = 38,
            AvgDaysOnMarket = 16,
            SaleToListRatio = 0.998,
            PriceRangeMin = 280_000m,
            PriceRangeMax = 750_000m,
            PrimaryCounties = """["Fairfax","Loudoun"]""",
            CommunityClients = "Nepali & Maithili-speaking"
        });
    }

    public override void Down()
    {
        Delete.FromTable(Tables.ProfessionalTrackRecords).Row(new { ProfessionalId = "5" });
        Delete.FromTable(Tables.ProfessionalTrackRecords).Row(new { ProfessionalId = "6" });
    }
}

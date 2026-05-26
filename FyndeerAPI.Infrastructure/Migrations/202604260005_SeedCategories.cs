using FluentMigrator;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Migrations;

[Migration(202604260005, "Seed Categories")]
public class Migration202604260005_SeedCategories : Migration
{
    public override void Up()
    {
        Insert.IntoTable(Tables.Categories).Row(new { Slug = "immigration-attorney", Name = "Immigration Attorney" });
        Insert.IntoTable(Tables.Categories).Row(new { Slug = "cpa-accountant", Name = "CPA / Accountant" });
        Insert.IntoTable(Tables.Categories).Row(new { Slug = "realtor", Name = "Realtor" });
        Insert.IntoTable(Tables.Categories).Row(new { Slug = "primary-care-doctor", Name = "Primary Care Doctor" });
        Insert.IntoTable(Tables.Categories).Row(new { Slug = "home-services", Name = "Home Services" });
    }

    public override void Down()
    {
        Delete.FromTable(Tables.Categories).Row(new { Slug = "immigration-attorney" });
        Delete.FromTable(Tables.Categories).Row(new { Slug = "cpa-accountant" });
        Delete.FromTable(Tables.Categories).Row(new { Slug = "realtor" });
        Delete.FromTable(Tables.Categories).Row(new { Slug = "primary-care-doctor" });
        Delete.FromTable(Tables.Categories).Row(new { Slug = "home-services" });
    }
}

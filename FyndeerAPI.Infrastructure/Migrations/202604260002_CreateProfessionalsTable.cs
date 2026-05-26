using FluentMigrator;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Migrations;

[Migration(202604260002, "Create Professionals table")]
public class Migration202604260002_CreateProfessionalsTable : Migration
{
    public override void Up()
    {
        Create.Table(Tables.Professionals)
            .WithColumn(Columns.General.Id).AsString(50).PrimaryKey().NotNullable()
            .WithColumn(Columns.Professionals.Slug).AsString(100).NotNullable()
            .WithColumn(Columns.Professionals.FullName).AsString(200).NotNullable()
            .WithColumn(Columns.Professionals.Title).AsString(150).NotNullable()
            .WithColumn(Columns.Professionals.CategoryId).AsString(100).NotNullable()
            .WithColumn(Columns.Professionals.CategoryName).AsString(200).NotNullable()
            .WithColumn(Columns.Professionals.YearsOfExperience).AsInt32().NotNullable()
            .WithColumn(Columns.Professionals.LicenseNumber).AsString(100).Nullable()
            .WithColumn(Columns.Professionals.Languages).AsString(int.MaxValue).NotNullable()
            .WithColumn(Columns.Professionals.Specialties).AsString(int.MaxValue).NotNullable()
            .WithColumn(Columns.Professionals.Phone).AsString(50).NotNullable()
            .WithColumn(Columns.Professionals.Email).AsString(255).NotNullable()
            .WithColumn(Columns.Professionals.Website).AsString(500).Nullable()
            .WithColumn(Columns.Professionals.ServiceArea).AsString(300).NotNullable()
            .WithColumn(Columns.Professionals.Bio).AsString(int.MaxValue).NotNullable()
            .WithColumn(Columns.Professionals.PhotoUrl).AsString(500).Nullable()
            .WithColumn(Columns.Professionals.IsSponsored).AsBoolean().NotNullable()
            .WithColumn(Columns.Professionals.IsVerified).AsBoolean().NotNullable()
            .WithColumn(Columns.Professionals.Rating).AsDouble().NotNullable()
            .WithColumn(Columns.Professionals.ReviewCount).AsInt32().NotNullable()
            .WithColumn(Columns.Professionals.LicenseState).AsString(100).Nullable()
            .WithColumn(Columns.Professionals.Brokerage).AsString(200).Nullable()
            .WithColumn(Columns.Professionals.IsAcceptingClients).AsBoolean().NotNullable()
            .WithColumn(Columns.Professionals.WorkingHours).AsString(100).Nullable();

        Create.Index(Indexes.Professionals.SlugUnique)
            .OnTable(Tables.Professionals)
            .OnColumn(Columns.Professionals.Slug)
            .Unique();

        Create.Index(Indexes.Professionals.EmailUnique)
            .OnTable(Tables.Professionals)
            .OnColumn(Columns.Professionals.Email)
            .Unique();

        Create.Index(Indexes.Professionals.CategoryId)
            .OnTable(Tables.Professionals)
            .OnColumn(Columns.Professionals.CategoryId);
    }

    public override void Down()
    {
        Delete.Table(Tables.Professionals);
    }
}

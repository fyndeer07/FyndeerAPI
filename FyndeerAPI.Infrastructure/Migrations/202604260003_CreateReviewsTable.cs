using FluentMigrator;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Migrations;

[Migration(202604260003, "Create Reviews table")]
public class Migration202604260003_CreateReviewsTable : Migration
{
    public override void Up()
    {
        Create.Table(Tables.Reviews)
            .WithColumn(Columns.General.Id).AsString(50).PrimaryKey().NotNullable()
            .WithColumn(Columns.Reviews.ProfessionalId).AsString(50).NotNullable()
            .WithColumn(Columns.Reviews.ReviewerName).AsString(200).NotNullable()
            .WithColumn(Columns.Reviews.IsVerified).AsBoolean().NotNullable()
            .WithColumn(Columns.Reviews.Rating).AsDouble().NotNullable()
            .WithColumn(Columns.Reviews.Comment).AsString(int.MaxValue).NotNullable()
            .WithColumn(Columns.General.CreatedAt).AsDateTime2().NotNullable();

        Create.Index(Indexes.Reviews.ProfessionalId)
            .OnTable(Tables.Reviews)
            .OnColumn(Columns.Reviews.ProfessionalId);

        Create.ForeignKey($"FK_{Tables.Reviews}_{Tables.Professionals}")
            .FromTable(Tables.Reviews).ForeignColumn(Columns.Reviews.ProfessionalId)
            .ToTable(Tables.Professionals).PrimaryColumn(Columns.General.Id)
            .OnDelete(System.Data.Rule.Cascade);
    }

    public override void Down()
    {
        Delete.Table(Tables.Reviews);
    }
}

using FluentMigrator;
using static FyndeerAPI.Infrastructure.Persistence.DatabaseSchema;

namespace FyndeerAPI.Infrastructure.Migrations;

[Migration(202604260001, "Create Categories table")]
public class Migration202604260001_CreateCategoriesTable : Migration
{
    public override void Up()
    {
        Create.Table(Tables.Categories)
            .WithColumn(Columns.General.Id).AsInt32().PrimaryKey().Identity()
            .WithColumn(Columns.Categories.Slug).AsString(100).NotNullable()
            .WithColumn(Columns.Categories.Name).AsString(200).NotNullable();

        Create.Index(Indexes.Categories.SlugUnique)
            .OnTable(Tables.Categories)
            .OnColumn(Columns.Categories.Slug)
            .Unique();
    }

    public override void Down()
    {
        Delete.Table(Tables.Categories);
    }
}

using FluentMigrator;

namespace SchoolManager.Repository.Migrations;
[Migration(1)]
public class InitialalMigration : Migration {
    public override void Up()
    {
        Create.Schema("demographics");
        Create.Table("students").InSchema("demographics")
            .WithColumn("id").AsInt32().Identity().PrimaryKey()
            .WithColumn("first_name").AsString(100).NotNullable()
            .WithColumn( "last_name").AsString(100).NotNullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
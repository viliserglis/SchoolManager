using FluentMigrator;

namespace SchoolManager.Repository.Migrations;

[Migration(3)]
public class AddedAcademicModule : Migration
{
    public override void Up()
    {
        Create.Schema("academic");
        Create.Table("courses").InSchema("academic")
            .WithColumn("id").AsInt32().Identity().PrimaryKey()
            .WithColumn("name").AsString(200).NotNullable()
            .WithColumn("room").AsString(50).NotNullable()
            .WithColumn("capacity").AsInt32().NotNullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
} 
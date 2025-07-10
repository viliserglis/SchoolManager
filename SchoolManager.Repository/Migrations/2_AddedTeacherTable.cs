using FluentMigrator;

namespace SchoolManager.Repository.Migrations;
[Migration(2)]
public class AddedTeacherTable : Migration {
    public override void Up()
    {
        Create.Table("Teacher").InSchema("demographics")
            .WithColumn("id").AsInt32().Identity().PrimaryKey()
            .WithColumn("first_name").AsString(100).NotNullable()
            .WithColumn( "last_name").AsString(100).NotNullable()
            .WithColumn( "qualification").AsString(100).NotNullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
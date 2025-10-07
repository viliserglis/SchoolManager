using FluentMigrator;

namespace SchoolManager.Repository.Migrations;
[Migration(6)]
public class AddedUserTable : Migration {
    public override void Up()
    {
        Create.Schema("auth");
        Create.Table("users").InSchema("auth")
            .WithColumn("id").AsInt32().Identity().PrimaryKey()
            .WithColumn("username").AsString(100).NotNullable().Unique()
            .WithColumn("password").AsString(255).NotNullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}

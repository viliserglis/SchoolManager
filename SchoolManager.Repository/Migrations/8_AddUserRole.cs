using FluentMigrator;
using SchoolManager.Repository.Constants;

namespace SchoolManager.Repository.Migrations;

[Migration(8)]
public class AddUserRole : Migration {
    public override void Up()
    {
        Alter.Table(TableConstants.User).InSchema(SchemaConstants.Auth).AddColumn(ColumnConstants.UserRole).AsInt32().Nullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
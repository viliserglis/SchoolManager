using FluentMigrator;
using SchoolManager.Repository.Constants;

namespace SchoolManager.Repository.Migrations;

[Migration(7)]
public class AddPermissionsAndRoles : Migration {
    public override void Up()
    {
        Create.Table(TableConstants.RolePermissions).InSchema(SchemaConstants.Auth)
            .WithColumn(ColumnConstants.Id).AsInt32().Identity().PrimaryKey()
            .WithColumn(ColumnConstants.PermissionCode).AsInt32().NotNullable()
            .WithColumn(ColumnConstants.RoleCode).AsInt32().NotNullable();
        
        Create.Table(TableConstants.Roles).InSchema(SchemaConstants.Auth)
            .WithColumn(ColumnConstants.Id).AsInt32().Identity().PrimaryKey()
            .WithColumn(ColumnConstants.UserId).AsInt32().NotNullable()
            .WithColumn(ColumnConstants.Code).AsInt32().NotNullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
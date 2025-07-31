using FluentMigrator;

namespace DefaultNamespace;

[Migration(4)]
public class CourseAddCollom : Migration {
    public override void Up()
    {
        Alter.Table("courses").InSchema("academic")
            .AddColumn("teacher_id").AsInt32();
        Create.ForeignKey()
            .FromTable("courses").InSchema("academic").ForeignColumn("teacher_id")
            .ToTable("Teacher").InSchema("demographics").PrimaryColumn("id");
    }
    
    public override void Down()
    {
        throw new NotImplementedException();
    }
}
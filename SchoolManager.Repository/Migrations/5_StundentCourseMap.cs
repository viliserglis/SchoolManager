using FluentMigrator;

namespace SchoolManager.Repository.Migrations;
[Migration (5)]
public class StundentCourseMap : Migration {
    public override void Up()
    {
        Create.Table("student_course").InSchema("academic")
            .WithColumn("id").AsInt32().Identity().PrimaryKey()
            .WithColumn("student_id").AsInt32().NotNullable()
            .WithColumn("course_id").AsInt32().NotNullable();
        Create.ForeignKey()
            .FromTable("student_course").InSchema("academic").ForeignColumn("course_id")
            .ToTable("courses").InSchema("academic").PrimaryColumn("id");
        Create.ForeignKey()
            .FromTable("student_course").InSchema("academic").ForeignColumn("student_id")
            .ToTable("students").InSchema("demographics").PrimaryColumn("id");
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
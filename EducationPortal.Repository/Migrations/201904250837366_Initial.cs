namespace EducationPortal.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Link = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Points = c.Int(nullable: false),
                        Course_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.UserSkills",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        SkillId = c.String(nullable: false, maxLength: 128),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SkillId })
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Login = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserCourses",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.String(nullable: false, maxLength: 128),
                        IsCompleted = c.Boolean(nullable: false),
                        Id = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.MaterialSkill",
                c => new
                    {
                        MaterialId = c.String(nullable: false, maxLength: 128),
                        SkillId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MaterialId, t.SkillId })
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.MaterialId)
                .Index(t => t.SkillId);
            
            CreateTable(
                "dbo.CourseMaterial",
                c => new
                    {
                        CourseId = c.String(nullable: false, maxLength: 128),
                        MaterialId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseId, t.MaterialId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PublishDate = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Author = c.String(),
                        PageNumber = c.Int(nullable: false),
                        Format = c.String(),
                        Year = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Video",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Length = c.String(),
                        Quality = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Video", "Id", "dbo.Materials");
            DropForeignKey("dbo.Book", "Id", "dbo.Materials");
            DropForeignKey("dbo.Article", "Id", "dbo.Materials");
            DropForeignKey("dbo.Skills", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.CourseMaterial", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.CourseMaterial", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.MaterialSkill", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.MaterialSkill", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.UserSkills", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserCourses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Materials", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserSkills", "SkillId", "dbo.Skills");
            DropIndex("dbo.Video", new[] { "Id" });
            DropIndex("dbo.Book", new[] { "Id" });
            DropIndex("dbo.Article", new[] { "Id" });
            DropIndex("dbo.CourseMaterial", new[] { "MaterialId" });
            DropIndex("dbo.CourseMaterial", new[] { "CourseId" });
            DropIndex("dbo.MaterialSkill", new[] { "SkillId" });
            DropIndex("dbo.MaterialSkill", new[] { "MaterialId" });
            DropIndex("dbo.UserCourses", new[] { "CourseId" });
            DropIndex("dbo.UserCourses", new[] { "UserId" });
            DropIndex("dbo.UserSkills", new[] { "SkillId" });
            DropIndex("dbo.UserSkills", new[] { "UserId" });
            DropIndex("dbo.Skills", new[] { "Course_Id" });
            DropIndex("dbo.Courses", new[] { "Owner_Id" });
            DropIndex("dbo.Materials", new[] { "User_Id" });
            DropTable("dbo.Video");
            DropTable("dbo.Book");
            DropTable("dbo.Article");
            DropTable("dbo.CourseMaterial");
            DropTable("dbo.MaterialSkill");
            DropTable("dbo.UserCourses");
            DropTable("dbo.Users");
            DropTable("dbo.UserSkills");
            DropTable("dbo.Skills");
            DropTable("dbo.Courses");
            DropTable("dbo.Materials");
        }
    }
}

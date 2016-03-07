namespace Com.Pinz.Server.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CompanyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        EMail = c.String(nullable: false),
                        FirstName = c.String(),
                        FamilyName = c.String(),
                        IsCompanyAdmin = c.Boolean(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.ProjectStaffs",
                c => new
                    {
                        ProjectId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        IsProjectAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.UserId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Guid(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                        Body = c.String(),
                        IsComplete = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DateCompleted = c.DateTime(),
                        StartDate = c.DateTime(),
                        DueDate = c.DateTime(),
                        ActualWork = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Priority = c.Int(),
                        CategoryId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tasks", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ProjectStaffs", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProjectStaffs", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Projects", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Categories", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "UserId" });
            DropIndex("dbo.Tasks", new[] { "CategoryId" });
            DropIndex("dbo.ProjectStaffs", new[] { "UserId" });
            DropIndex("dbo.ProjectStaffs", new[] { "ProjectId" });
            DropIndex("dbo.Users", new[] { "CompanyId" });
            DropIndex("dbo.Projects", new[] { "CompanyId" });
            DropIndex("dbo.Categories", new[] { "ProjectId" });
            DropTable("dbo.Tasks");
            DropTable("dbo.ProjectStaffs");
            DropTable("dbo.Users");
            DropTable("dbo.Companies");
            DropTable("dbo.Projects");
            DropTable("dbo.Categories");
        }
    }
}

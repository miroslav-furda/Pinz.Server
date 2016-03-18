namespace Com.Pinz.Server.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_PinzSuperadmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsPinzSuperAdmin", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "EMail", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Users", "EMail", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "EMail" });
            AlterColumn("dbo.Users", "EMail", c => c.String(nullable: false));
            DropColumn("dbo.Users", "IsPinzSuperAdmin");
        }
    }
}

namespace Com.Pinz.Server.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_Company_Admin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsCompanyAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsCompanyAdmin");
        }
    }
}

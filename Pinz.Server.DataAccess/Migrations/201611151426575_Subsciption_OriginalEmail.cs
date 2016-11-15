namespace Com.Pinz.Server.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subsciption_OriginalEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "OriginalEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscriptions", "OriginalEmail");
        }
    }
}

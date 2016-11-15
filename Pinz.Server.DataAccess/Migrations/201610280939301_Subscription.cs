namespace Com.Pinz.Server.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        SubscriptionReference = c.String(nullable: false, maxLength: 128),
                        Status = c.Int(nullable: false),
                        StatusChanged = c.DateTime(nullable: false),
                        StatusReason = c.Int(),
                        Cancelable = c.Boolean(nullable: false),
                        Test = c.Boolean(nullable: false),
                        Referrer = c.String(),
                        SourceName = c.String(),
                        SourceKey = c.String(),
                        SourceCampaign = c.String(),
                        CustomerUrl = c.String(),
                        ProductName = c.String(),
                        Tags = c.String(),
                        Quantity = c.Int(nullable: false),
                        Coupon = c.String(),
                        NextPeriodDate = c.DateTime(),
                        End = c.DateTime(),
                    })
                .PrimaryKey(t => t.SubscriptionReference);
            
            AddColumn("dbo.Companies", "SubscriptionReference", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Users", "PhoneNumber", c => c.String());
            CreateIndex("dbo.Companies", "SubscriptionReference");
            AddForeignKey("dbo.Companies", "SubscriptionReference", "dbo.Subscriptions", "SubscriptionReference", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "SubscriptionReference", "dbo.Subscriptions");
            DropIndex("dbo.Companies", new[] { "SubscriptionReference" });
            DropColumn("dbo.Users", "PhoneNumber");
            DropColumn("dbo.Companies", "SubscriptionReference");
            DropTable("dbo.Subscriptions");
        }
    }
}

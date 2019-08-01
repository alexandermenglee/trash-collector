namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SuspensionMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "StartSuspension", c => c.DateTime());
            AddColumn("dbo.Customers", "EndSuspendsion", c => c.DateTime());
            AddColumn("dbo.Customers", "AccountSuspended", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Customers", "SpecialPickupDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "SpecialPickupDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Customers", "AccountSuspended");
            DropColumn("dbo.Customers", "EndSuspendsion");
            DropColumn("dbo.Customers", "StartSuspension");
        }
    }
}

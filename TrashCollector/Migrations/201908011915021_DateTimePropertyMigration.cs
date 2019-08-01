namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimePropertyMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "SpecialPickupDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "SpecialPickupDate");
        }
    }
}

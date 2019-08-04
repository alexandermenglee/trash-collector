namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPickedUppropertytoCustomermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PickedUp", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "PickedUp");
        }
    }
}

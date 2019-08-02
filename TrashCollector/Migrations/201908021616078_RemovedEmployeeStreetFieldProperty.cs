namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedEmployeeStreetFieldProperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "Street");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Street", c => c.String());
        }
    }
}

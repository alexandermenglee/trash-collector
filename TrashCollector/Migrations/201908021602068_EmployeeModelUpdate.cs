namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "City", c => c.String());
            AddColumn("dbo.Employees", "Street", c => c.String());
            AddColumn("dbo.Employees", "State", c => c.String());
            AddColumn("dbo.Employees", "Zip", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Zip");
            DropColumn("dbo.Employees", "State");
            DropColumn("dbo.Employees", "Street");
            DropColumn("dbo.Employees", "City");
        }
    }
}

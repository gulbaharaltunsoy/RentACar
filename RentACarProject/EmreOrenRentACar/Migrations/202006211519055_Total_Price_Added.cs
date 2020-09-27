namespace EmreOrenRentACar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Total_Price_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "TotalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "TotalPrice");
        }
    }
}

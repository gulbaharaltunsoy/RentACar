namespace EmreOrenRentACar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicle", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Model = c.String(),
                        Year = c.Int(nullable: false),
                        Image = c.String(),
                        DailyPrice = c.Double(nullable: false),
                        VehicleTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleType", t => t.VehicleTypeId, cascadeDelete: true)
                .Index(t => t.VehicleTypeId);
            
            CreateTable(
                "dbo.VehicleType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicle", "VehicleTypeId", "dbo.VehicleType");
            DropForeignKey("dbo.Rentals", "VehicleId", "dbo.Vehicle");
            DropForeignKey("dbo.Rentals", "UserId", "dbo.User");
            DropIndex("dbo.Vehicle", new[] { "VehicleTypeId" });
            DropIndex("dbo.Rentals", new[] { "VehicleId" });
            DropIndex("dbo.Rentals", new[] { "UserId" });
            DropTable("dbo.VehicleType");
            DropTable("dbo.Vehicle");
            DropTable("dbo.User");
            DropTable("dbo.Rentals");
        }
    }
}

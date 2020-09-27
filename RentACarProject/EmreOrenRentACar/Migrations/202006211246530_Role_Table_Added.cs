namespace EmreOrenRentACar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role_Table_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "RoleId");
            AddForeignKey("dbo.User", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "RoleId", "dbo.Roles");
            DropIndex("dbo.User", new[] { "RoleId" });
            DropColumn("dbo.User", "RoleId");
            DropTable("dbo.Roles");
        }
    }
}

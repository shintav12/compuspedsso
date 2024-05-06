namespace CompuSPED.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class structure_change : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        DistrictId = c.Int(nullable: false, identity: true),
                        DistrictSourceId = c.String(),
                        DistrcitName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistrictId);
            
            CreateTable(
                "dbo.LoginHistory",
                c => new
                    {
                        LoginId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Email = c.String(),
                        Status = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LoginId);
            
            CreateTable(
                "dbo.School",
                c => new
                    {
                        SchoolId = c.Int(nullable: false, identity: true),
                        SchoolSourceId = c.String(),
                        SchoolCode = c.String(),
                        SchoolName = c.String(),
                    })
                .PrimaryKey(t => t.SchoolId);
            
            AddColumn("dbo.Clients", "ClientCode", c => c.String());
            DropTable("dbo.AccessToken");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.AccessToken",
                c => new
                    {
                        AccessTokenId = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        RefreshToken = c.String(),
                        ClientId = c.Int(nullable: false),
                        ExpiresIn = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccessTokenId);
            
            DropColumn("dbo.Clients", "ClientCode");
            DropTable("dbo.School");
            DropTable("dbo.LoginHistory");
            DropTable("dbo.Districts");
        }
    }
}

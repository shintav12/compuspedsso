namespace CompuSPED.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        ClientUID = c.String(),
                        ClientSecret = c.String(),
                        ClientCode = c.String(),
                        ClientASC = c.String(),
                        ClientIssuer = c.String(),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        DistrictId = c.Int(nullable: false, identity: true),
                        DistrictSBId = c.Int(nullable: false),
                        DistrictSourceId = c.String(),
                        DistrcitName = c.String(),
                        DistrictCode = c.String(),
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
                "dbo.ErrorLog",
                c => new
                    {
                        ErrorLogId = c.Int(nullable: false, identity: true),
                        ErrorType = c.String(),
                        Location = c.String(),
                        ErrorMessage = c.String(),
                        StackTrace = c.String(),
                        InnerException = c.String(),
                        RegisteredDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ErrorLogId);
            
            CreateTable(
                "dbo.School",
                c => new
                    {
                        SchoolId = c.Int(nullable: false, identity: true),
                        SchoolSourceId = c.String(),
                        SchoolSBId = c.Int(nullable: false),
                        SchoolCode = c.String(),
                        SchoolName = c.String(),
                        DistrcitId = c.String(),
                    })
                .PrimaryKey(t => t.SchoolId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.School");
            DropTable("dbo.ErrorLog");
            DropTable("dbo.LoginHistory");
            DropTable("dbo.Districts");
            DropTable("dbo.Clients");
        }
    }
}

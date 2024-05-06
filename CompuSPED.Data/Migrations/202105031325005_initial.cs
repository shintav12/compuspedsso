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
                    })
                .PrimaryKey(t => t.ClientID);
            
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.AccessToken");
            DropTable("dbo.Clients");
        }
    }
}

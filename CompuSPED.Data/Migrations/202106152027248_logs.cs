namespace CompuSPED.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ErrorLog",
                c => new
                    {
                        ErrorLogId = c.Int(nullable: false, identity: true),
                        ErrorType = c.String(),
                        Location = c.String(),
                        ErrorMessage = c.String(),
                    })
                .PrimaryKey(t => t.ErrorLogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ErrorLog");
        }
    }
}

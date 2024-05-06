namespace CompuSPED.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exceptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ErrorLog", "StackTrace", c => c.String());
            AddColumn("dbo.ErrorLog", "InnerException", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ErrorLog", "InnerException");
            DropColumn("dbo.ErrorLog", "StackTrace");
        }
    }
}

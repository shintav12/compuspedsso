namespace CompuSPED.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ErrorLog", "RegisteredDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ErrorLog", "RegisteredDate");
        }
    }
}

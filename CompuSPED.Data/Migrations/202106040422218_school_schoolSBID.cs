namespace CompuSPED.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class school_schoolSBID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.School", "SchoolSBId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.School", "SchoolSBId");
        }
    }
}

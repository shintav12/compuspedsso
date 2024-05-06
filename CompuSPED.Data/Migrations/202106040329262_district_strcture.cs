namespace CompuSPED.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class district_strcture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Districts", "DistrictSBId", c => c.Int(nullable: false));
            AlterColumn("dbo.Districts", "DistrcitName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Districts", "DistrcitName", c => c.Int(nullable: false));
            DropColumn("dbo.Districts", "DistrictSBId");
        }
    }
}

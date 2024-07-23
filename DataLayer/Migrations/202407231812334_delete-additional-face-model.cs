namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteadditionalfacemodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Faces", "Height");
            DropColumn("dbo.Faces", "Width");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Faces", "Width", c => c.Int(nullable: false));
            AddColumn("dbo.Faces", "Height", c => c.Int(nullable: false));
        }
    }
}

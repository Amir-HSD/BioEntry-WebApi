namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changefacemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faces", "Height", c => c.Int(nullable: false));
            AddColumn("dbo.Faces", "Width", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faces", "Width");
            DropColumn("dbo.Faces", "Height");
        }
    }
}

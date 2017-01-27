namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivityClassAndTypes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Likes", newName: "Activities");
            AddColumn("dbo.Activities", "ActivityType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "ActivityType");
            RenameTable(name: "dbo.Activities", newName: "Likes");
        }
    }
}

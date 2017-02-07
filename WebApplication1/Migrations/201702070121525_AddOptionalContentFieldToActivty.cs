namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOptionalContentFieldToActivty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "Content");
        }
    }
}

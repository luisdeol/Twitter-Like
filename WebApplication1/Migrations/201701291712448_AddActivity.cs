namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Activities");
            AddPrimaryKey("dbo.Activities", new[] { "UserId", "TweetId", "ActivityType" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Activities");
            AddPrimaryKey("dbo.Activities", new[] { "UserId", "TweetId" });
        }
    }
}

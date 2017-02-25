namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        ActorId = c.String(maxLength: 128),
                        UserId = c.String(),
                        TweetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ActorId)
                .ForeignKey("dbo.Tweets", t => t.TweetId, cascadeDelete: true)
                .Index(t => t.ActorId)
                .Index(t => t.TweetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "TweetId", "dbo.Tweets");
            DropForeignKey("dbo.Notifications", "ActorId", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "TweetId" });
            DropIndex("dbo.Notifications", new[] { "ActorId" });
            DropTable("dbo.Notifications");
        }
    }
}

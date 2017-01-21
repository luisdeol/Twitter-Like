namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DesperateTryCollectionsToTweet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Likes", "TweetId", "dbo.Tweets");
            AddForeignKey("dbo.Likes", "TweetId", "dbo.Tweets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "TweetId", "dbo.Tweets");
            AddForeignKey("dbo.Likes", "TweetId", "dbo.Tweets", "Id", cascadeDelete: true);
        }
    }
}

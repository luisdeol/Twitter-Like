namespace WebApplication1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Like : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        TweetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TweetId })
                .ForeignKey("dbo.Tweets", t => t.TweetId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TweetId);           
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "TweetId", "dbo.Tweets");
            DropIndex("dbo.Likes", new[] { "TweetId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropTable("dbo.Likes");
        }
    }
}

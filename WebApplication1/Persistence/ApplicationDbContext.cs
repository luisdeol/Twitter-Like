using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebApplication1.Core.Models;

namespace WebApplication1.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasMany(f=> f.Followees)
                .WithRequired(f=> f.Follower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(f=> f.Followers)
                .WithRequired(f=> f.Followee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tweet>()
                .HasMany(t=> t.Notifications)
                .WithRequired(t=> t.Tweet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tweet>()
                .HasMany(t => t.Activities)
                .WithRequired(t => t.Tweet)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}
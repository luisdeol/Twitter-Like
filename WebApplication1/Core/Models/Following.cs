using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Core.Models
{
    public class Following
    {
        [Key]
        [Column(Order = 1)]
        public int FolloweeId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int FollowerId { get; set; }

        public UserProfile Followee { get; set; }
        public UserProfile Follower { get; set; }
    }
}
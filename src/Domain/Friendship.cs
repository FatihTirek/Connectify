namespace Connectify.src.Domain
{
    public class Friendship
    {
        public int FollowerId { get; set; }
        public int FolloweeId { get; set; }
        public User Follower { get; set; } = null!; 
        public User Followee { get; set; } = null!;
    }
}
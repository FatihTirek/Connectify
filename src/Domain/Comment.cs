namespace Connectify.src.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int? RepliedCommentId { get; set; }
        public required string Content { get; set; }
        public int LikeCount { get; set; } = 0;
        public int? ReplyCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; } = null!;
        public Post Post { get; set; } = null!;
        public List<Like> Likes { get; } = [];
    }
}
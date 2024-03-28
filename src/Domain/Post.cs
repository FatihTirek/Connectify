namespace Connectify.src.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Content { get; set; }
        public int LikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public List<string> MediaURLs { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; } = null!;
        public List<Like> Likes { get; } = [];
        public List<Comment> Comments { get; } = [];
        public List<Tag> Tags { get; set; } = [];
    }
}
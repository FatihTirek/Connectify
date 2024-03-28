namespace Connectify.src.Domain
{
    public class Like
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public User User { get; set; } = null!;
        public Post Post { get; set; } = null!;
        public Comment Comment { get; set; } = null!;
    }
}
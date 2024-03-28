namespace Connectify.src.Domain
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public string? ProfilePhotoURL { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required DateOnly BirthDay { get; set; }
        public int FollowerCount { get; set; } = 0;
        public int FolloweeCount { get; set; } = 0;
        public bool IsMailVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public EmailConfirmationCode? EmailConfirmationCode { get; set; }
        public List<AuthToken> AuthTokens { get; set; } = [];
        public List<Post> Posts { get; } = [];
        public List<Like> Likes { get; } = [];
        public List<Comment> Comments { get; } = [];
        public List<Friendship> Followers { get; set; } = [];
        public List<Friendship> Following { get; set; } = [];
    }
}
namespace Connectify.src.Domain
{
    public class EmailConfirmationCode
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required string Email { get; set; }
        public required string Value { get; set; }
        public DateTime ExpireAt { get; set; } = DateTime.UtcNow.AddMinutes(30);
        public User User { get; set; } = null!;
    }
}
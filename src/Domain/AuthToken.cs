namespace Connectify.src.Domain
{
    public class AuthToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string DeviceId { get; set; }
        public required string Value { get; set; }
        public DateTime ExpireAt { get; set; } = DateTime.UtcNow.AddMinutes(30);
        public User User { get; set; } = null!;
    }
}
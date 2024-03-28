namespace Connectify.src.Domain
{
    public class Tag
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int PostCount { get; set; } = 0;
        public List<Post> Posts { get; } = [];
    }
}
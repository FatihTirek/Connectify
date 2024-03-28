using Connectify.src.Domain;
using Microsoft.EntityFrameworkCore;

namespace Connectify.src.Infrastructure.Persistence
{
    public class ProjectContext(DbContextOptions<ProjectContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<EmailConfirmationCode> EmailConfirmationCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => new { x.UserName })
                .HasMethod("GIN")
                .IsTsVectorExpressionIndex("english");

            modelBuilder.Entity<User>()
                .HasIndex(x => new { x.Email, x.UserName })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(x => x.AuthTokens)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(x => x.Posts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(x => x.Likes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasOne(x => x.EmailConfirmationCode)
                .WithOne(x => x.User)
                .HasForeignKey<EmailConfirmationCode>(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<Friendship>()
                .HasKey(x => new { x.FollowerId, x.FolloweeId });

            modelBuilder.Entity<Friendship>()
                .HasOne(x => x.Followee)
                .WithMany(x => x.Followers)
                .HasForeignKey(x => x.FollowerId);

            modelBuilder.Entity<Friendship>()
                .HasOne(x => x.Follower)
                .WithMany(x => x.Following)
                .HasForeignKey(x => x.FolloweeId);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Likes)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Tags)
                .WithMany(x => x.Posts)
                .UsingEntity("PostTag",
                    x => x.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagId").HasPrincipalKey(nameof(Tag.Id)),
                    x => x.HasOne(typeof(Post)).WithMany().HasForeignKey("PostId").HasPrincipalKey(nameof(Post.Id))
                );

            modelBuilder.Entity<Comment>()
                .HasMany(x => x.Likes)
                .WithOne(x => x.Comment)
                .HasForeignKey(x => x.CommentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tag>()
                .HasIndex(x => new { x.Name })
                .HasMethod("GIN")
                .IsTsVectorExpressionIndex("english");
        }
    }
}
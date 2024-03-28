using Connectify.src.Application.Repositories;
using Connectify.src.Domain;
using Microsoft.EntityFrameworkCore;

namespace Connectify.src.Infrastructure.Persistence.Repositories
{
    public class LikeRepository(ProjectContext context) : ILikeRepository
    {
        public async Task<bool> HasLikedPost(int userId, int postId)
        {
            return await context.Likes.AnyAsync(x => x.UserId == userId && x.PostId == postId);
        }

        public async Task<bool> HasLikedComment(int userId, int commentId)
        {
            return await context.Likes.AnyAsync(x => x.UserId == userId && x.CommentId == commentId);
        }

        public async Task Create(Like like)
        {
            await context.Likes.AddAsync(like);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int userId, int? postId = null, int? commentId = null)
        {
            if (postId != null) await context.Likes.Where(x => x.UserId == userId && x.PostId == postId).ExecuteDeleteAsync();
            else await context.Likes.Where(x => x.UserId == userId && x.CommentId == commentId).ExecuteDeleteAsync();
        }
    }
}
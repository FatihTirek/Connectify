using Connectify.src.Application.Repositories;
using Connectify.src.Domain;
using Connectify.src.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Connectify.Infrastructure.Persistence.Repositories
{
    public class CommentRepository(ProjectContext context) : ICommentRepository
    {
        private const int MaxCommentFetchSize = 15;
        private const int MaxReplyFetchSize = 10;

        public async Task<List<Comment>> GetRecentComments(int postId, int? lowestId)
        {
            if (lowestId == null) return await context.Comments.Where(x => x.PostId == postId).OrderByDescending(x => x.CreatedAt).Take(MaxCommentFetchSize).ToListAsync();
            return await context.Comments.Where(x => x.PostId == postId && x.Id < lowestId).OrderByDescending(x => x.CreatedAt).Take(MaxCommentFetchSize).ToListAsync();
        }

        public async Task<List<Comment>> GetRecentReplies(int repliedCommentId, int? lowestId)
        {
            if (lowestId == null) return await context.Comments.Where(x => x.RepliedCommentId == repliedCommentId).OrderByDescending(x => x.CreatedAt).Take(MaxReplyFetchSize).ToListAsync();
            return await context.Comments.Where(x => x.RepliedCommentId == repliedCommentId && x.Id < lowestId).OrderByDescending(x => x.CreatedAt).Take(MaxReplyFetchSize).ToListAsync();
        }

        public async Task Create(Comment comment)
        {
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
        }

        public async Task UpdateContent(int id, int userId, string content)
        {
            await context.Comments.Where(x => x.Id == id && x.UserId == userId).ExecuteUpdateAsync(x => x.SetProperty(x => x.Content, content));
        }

        public async Task Delete(int id, int userId)
        {
            await context.Comments.Where(x => x.Id == id && x.UserId == userId).ExecuteDeleteAsync();
        }
    }
}
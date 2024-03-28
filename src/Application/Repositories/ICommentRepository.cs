using Connectify.src.Domain;

namespace Connectify.src.Application.Repositories
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetRecentComments(int postId, int? lowestId);
        Task<List<Comment>> GetRecentReplies(int repliedCommentId, int? lowestId);
        Task Create(Comment comment);
        Task UpdateContent(int id, int userId, string content);
        Task Delete(int id, int userId);
    }
}
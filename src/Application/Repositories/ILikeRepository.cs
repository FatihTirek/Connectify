using Connectify.src.Domain;

namespace Connectify.src.Application.Repositories
{
    public interface ILikeRepository
    {
        Task<bool> HasLikedPost(int userId, int postId); 
        Task<bool> HasLikedComment(int userId, int commentId); 
        Task Create(Like like);
        Task Delete(int userId, int? postId = null, int? commentId = null);
    }
}
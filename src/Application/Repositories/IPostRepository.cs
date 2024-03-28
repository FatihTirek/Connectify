using Connectify.src.Application.DTOs.DB.Responses;
using Connectify.src.Domain;

namespace Connectify.src.Application.Repositories
{
    public interface IPostRepository
    {
        Task<List<GetPostsResponse>> GetRecentPosts(int? highestId, int? lowestId);
        Task<List<GetPostsResponse>> GetTagPosts(string tagName, int? lowestId);
        Task<List<string>> GetMediaURLsById(int postId);
        Task Create(Post post);
        Task UpdateContent(int id, int userId, string content);
        Task Delete(int id, int userId);
    }
}
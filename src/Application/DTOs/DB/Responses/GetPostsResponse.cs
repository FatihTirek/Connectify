using Connectify.src.Domain;

namespace Connectify.src.Application.DTOs.DB.Responses
{
    public record GetPostsResponse(Post Post, List<string> Tags);
}
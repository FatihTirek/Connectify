using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;

namespace Connectify.src.Application.Services
{
    public interface IPostService
    {
        Task<GetRecentPostsResponse> GetRecentPosts(GetRecentPostsRequest request);
        Task<GetTagPostsResponse> GetTagPosts(GetTagPostsRequest request);
        Task<CreatePostResponse> CreatePost(CreatePostRequest request);
        Task<UpdatePostResponse> UpdatePost(UpdatePostRequest request);
        Task<LikePostResponse> LikePost(LikePostRequest request);
        Task<UnlikePostResponse> UnlikePost(UnlikePostRequest request);
        Task<DeletePostResponse> DeletePost(DeletePostRequest request);
    }
}
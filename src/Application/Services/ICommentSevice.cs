using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;

namespace Connectify.src.Application.Services
{
    public interface ICommentService
    {
        Task<GetRecentCommentsResponse> GetRecentComments(GetRecentCommentsRequest request);
        Task<GetRecentRepliesResponse> GetRecentReplies(GetRecentRepliesRequest request);
        Task<CreateCommentResponse> CreateComment(CreateCommentRequest request);
        Task<UpdateCommentResponse> UpdateComment(UpdateCommentRequest request);
        Task<LikeCommentResponse> LikeComment(LikeCommentRequest request);
        Task<UnlikeCommentResponse> UnlikeComment(UnlikeCommentRequest request);
        Task<DeleteCommentResponse> DeleteComment(DeleteCommentRequest request);
    }
}
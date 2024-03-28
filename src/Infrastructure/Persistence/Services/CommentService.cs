using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Repositories;
using Connectify.src.Application.Services;
using Connectify.src.Domain;


namespace Connectify.src.Infrastructure.Persistence.Services
{
    public class CommentService(ICommentRepository commentRepository, ILikeRepository likeRepository) : ICommentService
    {
        public async Task<GetRecentCommentsResponse> GetRecentComments(GetRecentCommentsRequest request)
        {
            var result = await commentRepository.GetRecentComments(request.PostId, request.LowestId);
            return new GetRecentCommentsResponse.Success(result);
        }

        public async Task<GetRecentRepliesResponse> GetRecentReplies(GetRecentRepliesRequest request)
        {
            var result = await commentRepository.GetRecentReplies(request.RepliedCommentId, request.LowestId);
            return new GetRecentRepliesResponse.Success(result);
        }

        public async Task<CreateCommentResponse> CreateComment(CreateCommentRequest request)
        {
            var comment = new Comment
            {
                UserId = request.UserId,
                PostId = request.PostId,
                RepliedCommentId = request.RepliedCommentId,
                Content = request.Content,
            };

            await commentRepository.Create(comment);
            return new CreateCommentResponse.Success();
        }

        public async Task<UpdateCommentResponse> UpdateComment(UpdateCommentRequest request)
        {
            await commentRepository.UpdateContent(request.CommentId, request.UserId, request.Content);
            return new UpdateCommentResponse.Success();
        }

        public async Task<LikeCommentResponse> LikeComment(LikeCommentRequest request)
        {
            var result = await likeRepository.HasLikedComment(request.UserId, request.CommentId);
            if (!result) await likeRepository.Create(new Like { UserId = request.UserId, CommentId = request.CommentId });

            return new LikeCommentResponse.Success();
        }

        public async Task<UnlikeCommentResponse> UnlikeComment(UnlikeCommentRequest request)
        {
            var result = await likeRepository.HasLikedComment(request.UserId, request.CommentId);
            if (result) await likeRepository.Delete(request.UserId, commentId: request.CommentId);

            return new UnlikeCommentResponse.Success();
        }

        public async Task<DeleteCommentResponse> DeleteComment(DeleteCommentRequest request)
        {
            await commentRepository.Delete(request.CommentId, request.UserId);
            return new DeleteCommentResponse.Success();
        }
    }
}
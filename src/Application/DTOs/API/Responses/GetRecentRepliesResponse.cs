using Connectify.src.Application.Base;
using Connectify.src.Domain;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record GetRecentRepliesResponse
    {
        public record Success : GetRecentRepliesResponse
        {
            public IEnumerable<object> Replies { get; }

            public Success(IEnumerable<Comment> Replies) => this.Replies = Replies.Select(x => new
            {
                x.Id,
                x.UserId,
                x.RepliedCommentId,
                x.Content,
                x.LikeCount,
                x.CreatedAt,
            });
        }

        public record Failure(IEnumerable<ValidationError> Errors) : GetRecentRepliesResponse;
    }
}
using Connectify.src.Application.Base;
using Connectify.src.Domain;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record GetRecentCommentsResponse
    {
        public record Success : GetRecentCommentsResponse
        {
            public IEnumerable<object> Comments { get; }

            public Success(IEnumerable<Comment> Comments) => this.Comments = Comments.Select(x => new
            {
                x.Id,
                x.UserId,
                x.Content,
                x.LikeCount,
                x.ReplyCount,
                x.CreatedAt,
            });
        }

        public record Failure(IEnumerable<ValidationError> Errors) : GetRecentCommentsResponse;
    }
}
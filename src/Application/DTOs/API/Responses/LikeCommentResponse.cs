using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record LikeCommentResponse
    {
        public record Success : LikeCommentResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : LikeCommentResponse;
    }
}
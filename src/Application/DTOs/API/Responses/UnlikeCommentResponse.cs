using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record UnlikeCommentResponse
    {
        public record Success : UnlikeCommentResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : UnlikeCommentResponse;
    }
}
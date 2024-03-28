using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record DeleteCommentResponse
    {
        public record Success : DeleteCommentResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : DeleteCommentResponse;
    }
}
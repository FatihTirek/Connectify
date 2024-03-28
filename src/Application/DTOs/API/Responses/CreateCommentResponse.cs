using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record CreateCommentResponse
    {
        public record Success : CreateCommentResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : CreateCommentResponse;
    }
}
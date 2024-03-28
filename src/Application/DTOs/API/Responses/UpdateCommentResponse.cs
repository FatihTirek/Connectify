using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record UpdateCommentResponse
    {
        public record Success : UpdateCommentResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : UpdateCommentResponse;
    }
}
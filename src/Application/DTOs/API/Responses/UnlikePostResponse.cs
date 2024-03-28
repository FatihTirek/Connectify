using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record UnlikePostResponse
    {
        public record Success : UnlikePostResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : UnlikePostResponse;
    }
}
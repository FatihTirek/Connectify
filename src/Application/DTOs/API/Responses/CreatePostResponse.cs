using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record CreatePostResponse
    {
        public record Success : CreatePostResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : CreatePostResponse;
    }
}
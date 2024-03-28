using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record LikePostResponse
    {
        public record Success : LikePostResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : LikePostResponse;
    }
}
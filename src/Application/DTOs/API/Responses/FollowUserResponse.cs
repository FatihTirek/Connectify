using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record FollowUserResponse
    {
        public record Success : FollowUserResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : FollowUserResponse;
    }
}
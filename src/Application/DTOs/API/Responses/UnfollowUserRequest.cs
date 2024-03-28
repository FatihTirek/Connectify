using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record UnfollowUserResponse
    {
        public record Success : UnfollowUserResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : UnfollowUserResponse;
    }
}
using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record LogOutResponse
    {
        public record Success : LogOutResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : LogOutResponse;
    }
}
using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record UpdateProfileResponse
    {
        public record Success : UpdateProfileResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : UpdateProfileResponse;
    }
}
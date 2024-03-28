using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record SendConfirmationCodeResponse
    {
        public record Success : SendConfirmationCodeResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : SendConfirmationCodeResponse;
    }
}
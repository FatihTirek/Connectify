using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record VerifyConfirmationCodeResponse
    {
        public record Success(string AuthToken) : VerifyConfirmationCodeResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : VerifyConfirmationCodeResponse;
    }
}
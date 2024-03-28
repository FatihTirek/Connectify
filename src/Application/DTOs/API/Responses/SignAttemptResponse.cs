using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record SignAttemptResponse
    {
        public record Success(string AuthToken) : SignAttemptResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : SignAttemptResponse;
    }
}
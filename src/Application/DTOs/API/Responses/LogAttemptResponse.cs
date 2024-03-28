using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record LogAttemptResponse
    {
        public record Success(string AuthToken, bool IsMailVerified) : LogAttemptResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : LogAttemptResponse;
    }
}
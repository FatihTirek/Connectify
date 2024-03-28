using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record RefreshAuthTokenResponse
    {
        public record Success(string AuthToken) : RefreshAuthTokenResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : RefreshAuthTokenResponse;
    }
}
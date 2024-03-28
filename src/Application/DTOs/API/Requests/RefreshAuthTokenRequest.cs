namespace Connectify.src.Application.DTOs.API.Requests
{
    public record RefreshAuthTokenRequest
    {
        public required string AuthToken { get; set; }
    }
}
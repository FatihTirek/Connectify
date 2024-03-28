namespace Connectify.src.Application.DTOs.DB.Responses
{
    public record GetAuthTokenResponse(int UserId, string DeviceId, bool IsMailVerified, DateTime ExpireAt);
}
namespace Connectify.src.Application.DTOs.DB.Responses
{
    public record GetUserByEmailResponse(int UserId, string PasswordHash, bool IsMailVerified);
}
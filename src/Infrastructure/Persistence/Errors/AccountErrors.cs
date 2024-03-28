using Connectify.src.Application.Base;

namespace Connectify.src.Infrastructure.Persistence.Errors
{
    public record AccountError(string ErrorCode, string ErrorMessage) : ValidationError(ErrorCode, ErrorMessage)
    {
        public record EmailExists() : AccountError("EmailExists", "Email already exists");
        public record UserNameExists() : AccountError("UserNameExists", "UserName already exists");
        public record YoungerThanThirteen() : AccountError("YoungerThanThirteen", "User must be at least 13 years old");
        public record InvalidCredentials() : AccountError("InvalidCredentials", "Email or password is incorrect");
    }
}
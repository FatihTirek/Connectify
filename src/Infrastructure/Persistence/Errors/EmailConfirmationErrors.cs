using Connectify.src.Application.Base;

namespace Connectify.src.Infrastructure.Persistence.Errors
{
    public record EmailConfirmationError(string ErrorCode, string ErrorMessage) : ValidationError(ErrorCode, ErrorMessage)
    {
        public record CodeNotExists() : EmailConfirmationError("ConfirmationCodeNotExists", "Confirmation code does not exists");
        public record InvalidCode() : EmailConfirmationError("InvalidConfirmationCode", "Invalid confirmation code");
        public record CodeExpired() : EmailConfirmationError("ConfirmationCodeExpired", "Confirmation code has expired");
    }
}
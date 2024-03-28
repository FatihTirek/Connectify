using Connectify.src.Application.Base;

namespace Connectify.src.Infrastructure.Persistence.Errors
{
    public record PostError(string ErrorCode, string ErrorMessage) : ValidationError(ErrorCode, ErrorMessage)
    {
        public record InvalidFileType() : PostError("InvalidFileType", "Invalid file type.");
    }
}
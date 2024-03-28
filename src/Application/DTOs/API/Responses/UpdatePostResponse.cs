using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record UpdatePostResponse
    {
        public record Success : UpdatePostResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : UpdatePostResponse;
    }
}
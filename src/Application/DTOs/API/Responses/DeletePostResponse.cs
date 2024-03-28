using Connectify.src.Application.Base;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record DeletePostResponse
    {
        public record Success : DeletePostResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : DeletePostResponse;
    }
}
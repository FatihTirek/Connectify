using Connectify.src.Application.Base;
using Connectify.src.Application.DTOs.DB.Responses;
using Connectify.src.Domain;

namespace Connectify.src.Application.DTOs.API.Responses
{
    public record SearchResponse
    {
        public record Success(IEnumerable<GetUsersBySearchResponse> Users, IEnumerable<Tag> Tags) : SearchResponse;

        public record Failure(IEnumerable<ValidationError> Errors) : SearchResponse;
    }
}
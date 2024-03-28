using Connectify.src.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record SearchRequest : CookieRequest
    {
        [FromQuery]
        public required string Query { get; set; }
    }
}
using Connectify.src.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record GetRecentCommentsRequest : CookieRequest
    {
        [FromQuery]
        public required int PostId { get; set; }
        [FromQuery]
        public int? LowestId { get; set; }
    }
}
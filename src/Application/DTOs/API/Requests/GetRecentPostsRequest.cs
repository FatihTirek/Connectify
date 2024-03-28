using Connectify.src.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record GetRecentPostsRequest : CookieRequest
    {
        [FromQuery]
        public int? HighestId { get; set; }
        [FromQuery]
        public int? LowestId { get; set; }
    }
}
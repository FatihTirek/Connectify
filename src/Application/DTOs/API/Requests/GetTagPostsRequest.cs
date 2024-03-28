using Connectify.src.Application.Base;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record GetTagPostsRequest : CookieRequest
    {
        [FromQuery]
        public required string TagName { get; set; }
        [FromQuery]
        public int? LowestId { get; set; }
    }
}
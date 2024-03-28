using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Connectify.src.Application.DTOs.API.Requests
{
    public record LogOutRequest
    {
        [JsonIgnore, BindNever]
        public string? AuthToken { get; set; }
        [FromBody]
        public required string DeviceId { get; set; }
    }
}
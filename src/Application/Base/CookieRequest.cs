using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Connectify.src.Application.Base
{
    public abstract record CookieRequest
    {
        [JsonIgnore, BindNever]
        public int UserId { get; set; }
    }
}
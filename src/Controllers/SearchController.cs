using Connectify.src.Application.Attributes;
using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController(ISearchService searchService) : ControllerBase
    {
        [ServiceFilter(typeof(ValidateUser<SearchRequest>))]
        [HttpPost]
        public async Task<IActionResult> Search(SearchRequest request)
        {
            var response = await searchService.Search(request);
            if (response.GetType() == typeof(FollowUserResponse.Failure)) return BadRequest(response);

            return Ok(response);
        }
    }
}

using System.Diagnostics.CodeAnalysis;
using Connectify.src.Application.Attributes;
using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController(IPostService postService) : ControllerBase
    {
        [ServiceFilter(typeof(ValidateUser<GetRecentPostsRequest>))]
        [HttpGet]
        public async Task<IActionResult> GetRecentPosts(GetRecentPostsRequest request)
        {
            var response = await postService.GetRecentPosts(request);
            if (response.GetType() == typeof(GetRecentPostsResponse.Failure)) return BadRequest(response);
            return Ok(response);
        }

        [ServiceFilter(typeof(ValidateUser<GetTagPostsRequest>))]
        [HttpGet("tag-posts")]
        public async Task<IActionResult> GetTagPosts(GetTagPostsRequest request)
        {
            var response = await postService.GetTagPosts(request);
            if (response.GetType() == typeof(GetTagPostsResponse.Failure)) return BadRequest(response);
            return Ok(response);
        }

        [ValidateRequest<CreatePostRequest, CreatePostValidator>(Order = 2)]
        [ServiceFilter(typeof(ValidateUser<CreatePostRequest>), Order = 1)]
        [HttpPost("create")]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            var response = await postService.CreatePost(request);
            if (response.GetType() == typeof(CreatePostResponse.Failure)) return BadRequest(response);
            return Created();
        }

        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ValidateRequest<UpdatePostRequest, UpdatePostValidator>(Order = 2)]
        [ServiceFilter(typeof(ValidateUser<UpdatePostRequest>), Order = 1)]
        [HttpPut("{PostId}")]
        public async Task<IActionResult> UpdatePost(UpdatePostRequest request)
        {
            var response = await postService.UpdatePost(request);
            if (response.GetType() == typeof(UpdatePostResponse.Failure)) return BadRequest(response);
            return NoContent();
        }

        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ServiceFilter(typeof(ValidateUser<LikePostRequest>))]
        [HttpPost("{PostId}/like")]
        public async Task<IActionResult> LikePost(LikePostRequest request)
        {
            var response = await postService.LikePost(request);
            if (response.GetType() == typeof(LikePostResponse.Failure)) return BadRequest(response);
            return NoContent();
        }

        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ServiceFilter(typeof(ValidateUser<UnlikePostRequest>))]
        [HttpPost("{PostId}/unlike")]
        public async Task<IActionResult> UnlikePost(UnlikePostRequest request)
        {
            var response = await postService.UnlikePost(request);
            if (response.GetType() == typeof(UnlikePostResponse.Failure)) return BadRequest(response);
            return NoContent();
        }

        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ServiceFilter(typeof(ValidateUser<DeletePostRequest>))]
        [HttpDelete("{PostId}")]
        public async Task<IActionResult> DeletePost(DeletePostRequest request)
        {
            var response = await postService.DeletePost(request);
            if (response.GetType() == typeof(DeletePostResponse.Failure)) return BadRequest(response);
            return NoContent();
        }
    }
}
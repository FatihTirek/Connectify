using System.Diagnostics.CodeAnalysis;
using Connectify.src.Application.Attributes;
using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.src.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
        [ServiceFilter(typeof(ValidateUser<GetRecentCommentsRequest>))]
        [HttpGet]
        public async Task<IActionResult> GetRecentComments(GetRecentCommentsRequest request)
        {
            var response = await commentService.GetRecentComments(request);
            if (response.GetType() == typeof(GetRecentCommentsResponse.Failure)) return BadRequest(response);
            return Ok(response);
        }

        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ServiceFilter(typeof(ValidateUser<GetRecentRepliesRequest>))]
        [HttpGet("{RepliedCommentId}/replies")]
        public async Task<IActionResult> GetRecentReplies(GetRecentRepliesRequest request)
        {
            var response = await commentService.GetRecentReplies(request);
            if (response.GetType() == typeof(GetRecentRepliesResponse.Failure)) return BadRequest(response);
            return Ok(response);
        }

        [ValidateRequest<CreateCommentRequest, CreateCommentValidator>(Order = 2)]
        [ServiceFilter(typeof(ValidateUser<CreateCommentRequest>), Order = 1)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateComment(CreateCommentRequest request)
        {
            var response = await commentService.CreateComment(request);
            if (response.GetType() == typeof(CreateCommentResponse.Failure)) return BadRequest(response);
            return Created();
        }

        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ValidateRequest<UpdateCommentRequest, UpdateCommentValidator>(Order = 2)]
        [ServiceFilter(typeof(ValidateUser<UpdateCommentRequest>), Order = 1)]
        [HttpPut("{CommentId}")]
        public async Task<IActionResult> UpdateComment(UpdateCommentRequest request)
        {
            var response = await commentService.UpdateComment(request);
            if (response.GetType() == typeof(UpdateCommentResponse.Failure)) return BadRequest(response);
            return NoContent();
        }

        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ServiceFilter(typeof(ValidateUser<LikeCommentRequest>))]
        [HttpPost("{CommentId}/like")]
        public async Task<IActionResult> LikeComment(LikeCommentRequest request)
        {
            var response = await commentService.LikeComment(request);
            if (response.GetType() == typeof(LikeCommentResponse.Failure)) return BadRequest(response);
            return NoContent();
        }

        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ServiceFilter(typeof(ValidateUser<UnlikeCommentRequest>))]
        [HttpPost("{CommentId}/unlike")]
        public async Task<IActionResult> UnlikeComment(UnlikeCommentRequest request)
        {
            var response = await commentService.UnlikeComment(request);
            if (response.GetType() == typeof(UnlikeCommentResponse.Failure)) return BadRequest(response);
            return NoContent();
        }

        [SuppressMessage("Usage", "ASP0018:Unused route parameter", Justification = "<Pending>")]
        [ServiceFilter(typeof(ValidateUser<DeleteCommentRequest>))]
        [HttpDelete("{CommentId}")]
        public async Task<IActionResult> DeleteComment(DeleteCommentRequest request)
        {
            var response = await commentService.DeleteComment(request);
            if (response.GetType() == typeof(DeleteCommentResponse.Failure)) return BadRequest(response);
            return NoContent();
        }
    }
}
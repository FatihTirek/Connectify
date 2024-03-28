using Connectify.src.Application.DTOs.API.Requests;
using Connectify.src.Application.DTOs.API.Responses;
using Connectify.src.Application.Repositories;
using Connectify.src.Application.Services;
using Connectify.src.Domain;
using Connectify.src.Infrastructure.Persistence.Errors;


namespace Connectify.src.Infrastructure.Persistence.Services
{
    public class PostService(IPostRepository postRepository, ITagRepository tagRepository, ILikeRepository likeRepository) : IPostService
    {
        public async Task<GetRecentPostsResponse> GetRecentPosts(GetRecentPostsRequest request)
        {
            var data = await postRepository.GetRecentPosts(request.HighestId, request.LowestId);
            return new GetRecentPostsResponse.Success(data);
        }

        public async Task<GetTagPostsResponse> GetTagPosts(GetTagPostsRequest request)
        {
            var data = await postRepository.GetTagPosts(request.TagName, request.LowestId);
            return new GetTagPostsResponse.Success(data);
        }

        public async Task<CreatePostResponse> CreatePost(CreatePostRequest request)
        {
            var isInvalidFileType = false;
            var mediaURLs = new List<string>();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var root = Path.Combine(Directory.GetCurrentDirectory(), "img");

            Directory.CreateDirectory(root);

            request.Medias.ForEach(async item =>
            {
                var extension = Path.GetExtension(item.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    isInvalidFileType = true; 
                    return;
                }

                var path = Path.Combine(root, $"{Guid.NewGuid()}{extension}");
                using var stream = new FileStream(path, FileMode.Create);
                await item.CopyToAsync(stream); mediaURLs.Add(path);
            });

            if (isInvalidFileType) return new CreatePostResponse.Failure([new PostError.InvalidFileType()]);

            var oldTags = await tagRepository.GetByNames(request.Tags);
            var newTags = request.Tags.Except(oldTags.Select(x => x.Name).Intersect(request.Tags)).Select(x => new Tag { Name = x }).ToList();

            var post = new Post
            {
                UserId = request.UserId,
                Content = request.Content,
                MediaURLs = mediaURLs,
                Tags = [.. newTags, .. oldTags],
            };

            await postRepository.Create(post);
            return new CreatePostResponse.Success();
        }

        public async Task<UpdatePostResponse> UpdatePost(UpdatePostRequest request)
        {
            await postRepository.UpdateContent(request.PostId, request.UserId, request.Content);
            return new UpdatePostResponse.Success();
        }

        public async Task<LikePostResponse> LikePost(LikePostRequest request)
        {
            var result = await likeRepository.HasLikedPost(request.UserId, request.PostId);
            if (!result) await likeRepository.Create(new Like { UserId = request.UserId, PostId = request.PostId });

            return new LikePostResponse.Success();
        }

        public async Task<UnlikePostResponse> UnlikePost(UnlikePostRequest request)
        {
            var result = await likeRepository.HasLikedPost(request.UserId, request.PostId);
            if (result) await likeRepository.Delete(request.UserId, postId: request.PostId);
            return new UnlikePostResponse.Success();
        }

        public async Task<DeletePostResponse> DeletePost(DeletePostRequest request)
        {
            (await postRepository.GetMediaURLsById(request.PostId)).ForEach(File.Delete);
            await postRepository.Delete(request.PostId, request.UserId);
            return new DeletePostResponse.Success();
        }
    }
}
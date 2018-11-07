using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Models.Forum;
using ForumApp.Models.Post;
using ForumApp.Models.PostReply;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ForumApp.Controllers
{
    public class PostController: Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;

        public PostController(IForum forumService, IPost postService)
        {
            _forumService = forumService;
            _postService = postService;
        }

        public IActionResult Index(int postId)
        {
            var post = _postService.GetPostById(postId);
            // For now it returns NULL as posts do not exist
            // Wonder if there is a way to fix it and return an empty page?
            var replies = BuildPostRepliesModel(post.Replies);

            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                Created = post.Created.ToLocalTime().ToString("d"),
                Content = post.Content,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImgUrl = post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                Forum = BuildForumListing(post.Forum),
                PostReplies = replies,
                RepliesCount = post.Replies.Count()
            };

            return View(model);
        }

        private IEnumerable<PostReplyModel> BuildPostRepliesModel(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorId = reply.User.Id ,
                AuthorName = reply.User.UserName,
                AuthorRating = reply.User.Rating ,
                AuthorImgUrl =reply.User.ProfileImageUrl,
                Created = reply.Created.ToLocalTime().ToString("d"),
                ReplyContent = reply.Content,
                PostId = reply.Post.Id
            });
        }

        private ForumListingModel BuildForumListing(Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }


    }
}

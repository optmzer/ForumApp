using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Models.Forum;
using ForumApp.Models.Post;
using ForumApp.Models.PostReply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Controllers
{
    public class PostController: Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(IForum forumService
                            , IPost postService
                            , UserManager<ApplicationUser> userManager)
        {
            _forumService = forumService;
            _postService = postService;
            _userManager = userManager;
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

        /**
         * Creates new Post in a Forum determined by forumId
         */ 
        public IActionResult CreateNewPost(int forumId)
        {
            var forum = _forumService.GetById(forumId);

            var model = new NewPostModel
            {
                ForumId = forum.Id,
                ForumName = forum.Title,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            //_userManager is a built in service. From
            // Microsoft.AspNetCore.Identity; - Provides API to interact with Users in
            //Data store.
            //User is a built in Object that contains Current User info.
            var user = await _userManager.FindByIdAsync(userId);
            var post = BuildPost(model, user);
            //TODO: User management rating.

            await _postService.AddPost(post);

            return RedirectToAction("Index", "Post", post.Id);
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _forumService.GetById(model.ForumId);

            return new Post
            {
                Title = model.PostTitle,
                Content = model.PostContent,
                Created = DateTime.Now,
                User = user,
                Forum = forum
            };
        }

        // ===== Private Methods
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

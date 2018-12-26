using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Models.PostReply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Controllers
{
    public class PostReplyController : Controller
    {
        private readonly IApplicationUser _userService;
        private readonly IPost _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostReplyController(
              IPost postService
            , UserManager<ApplicationUser> userManager
            , IApplicationUser userService)
        {
            _postService = postService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> CreateNewReply(int postId) {

            var post = _postService.GetPostById(postId);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new PostReplyModel
            {
                AuthorId = user.Id,
                AuthorName = user.UserName,
                AuthorRating = user.Rating,
                AuthorImgUrl = user.ProfileImageUrl,

                IsAuthorAdmin = User.IsInRole("Admin"),

                PostId = post.Id,
                PostTitle = post.Title,
                PostContent = post.Content,

                ForumId = post.Forum.Id,
                ForumTitle = post.Forum.Title,
                ForumImageUrl = post.Forum.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPostReply(PostReplyModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var reply = BuildReply(user, model);

            await _postService.AddNewPostReply(reply);
            await _userService.UpdateUserRating(userId, typeof(PostReply));

            return RedirectToAction("Index", "Post", new { postId = model.PostId});
        }

        private PostReply BuildReply(ApplicationUser user, PostReplyModel model)
        {
            return new PostReply
            {
                Content = model.ReplyContent,
                User = user,
                Post = _postService.GetPostById(model.PostId)
            };
        }
    }
}

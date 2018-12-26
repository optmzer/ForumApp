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
        private readonly IPost _postService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostReplyController(
              IPost postService
            , UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        public async Task<IActionResult> CreateNewReply(int postId) {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPostReply(PostReplyModel model)
        {

            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(user, model);
            await _postService.AddNewPostReply(reply);

            var postId = model.PostId;

            return RedirectToAction("Index", "Post", new { postId});
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

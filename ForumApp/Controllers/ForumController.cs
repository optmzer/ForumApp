using System.Linq;
using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Models.Forum;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;

        public ForumController(IForum forumService, IPost postService)
        {
            _forumService = forumService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAll().Select(forum => new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            return View(model);
        }

        public IActionResult Topic(int forumId, string searchQuery)
        {
            var forum = _forumService.GetById(forumId);
            var posts = _postService.GetFilteredPosts(forum, searchQuery).ToList();
            
            //Convert forum into list of posts.
            var postList = posts.Select(p => new ForumTopicListingModel
            {
                Id = p.Id,
                Title = p.Title,
                AuthorId = p.User.Id,
                AuthorName = p.User.UserName,
                AuthorRating = p.User.Rating,
                Created = p.Created.ToLocalTime().ToString("d"),
                RepliesCount = p.Replies.Count(),
                Forum = BuildForumListing(p)
            });

            var model = new ForumTopicModel
            {
                Forum = BuildForumListing(forum),
                PostList = postList
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Search(int forumId, string searchQuery)
        {
            return RedirectToAction("Topic", new {forumId, searchQuery });
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;

            return BuildForumListing(forum);
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
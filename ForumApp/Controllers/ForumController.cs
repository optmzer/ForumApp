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

            var model = new ForumIndexModel()
            {
                ForumList = forums
            };

            return View(model);
        }

        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);

            //Convert forum into list of posts.
            var posts = forum.Posts.Select(p => new ForumListingModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Content
                //AuthorId = p.User.Id,
                //AuthorName = p.User.UserName,
                //AuthorRating = p.User.Rating,
                //Created = p.Created.ToLocalTime().ToString("d"),
                //RepliesCount = p.Replies.Count(),
                //Forum = BuildForumListing(p)
            });

            //var posts = _postService.GetPostsByForum(id);

            //var postListing = posts.Select(p => new PostListingModel
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    AuthorId = p.User.Id,
            //    AuthorName = p.User.UserName,
            //    AuthorRating = p.User.Rating,
            //    Created = p.Created.ToLocalTime().ToString("d"),
            //    RepliesCount = p.Replies.Count(),
            //    Forum = BuildForumListing(p)
            //});


            var model = new ForumTopicModel
            {
                Forum = BuildForumListing(forum),
                PostList = posts
            };

            return View(model);
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
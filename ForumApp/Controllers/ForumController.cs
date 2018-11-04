using System.Collections.Generic;
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
        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAll().Select(forum => new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description
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

            var posts = forum.Posts.Select(post => new ForumPostListingModel
            {
                Id = post.Id,
                Created = post.Created,
                Title = post.Title,
                Content = post.Content,
                User = post.User
                //Later add number of replies
            });

            var model = new ForumTopicModel
            {
                TopicTitle = forum.Title,
                PostList = posts
            };

            return View(model);
        }
    }
}
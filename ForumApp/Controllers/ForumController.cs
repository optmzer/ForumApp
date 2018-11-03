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
            var topic = _forumService.GetById(id);

            return View();
        }
    }
}
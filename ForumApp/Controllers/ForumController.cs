﻿using System.Collections.Generic;
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

            //var posts = forum.Posts.Select(post => new PostListingModel
            //{
            //    Id = post.Id,
            //    Title = post.Title,
            //    Created = post.Created.ToLocalTime().ToString("d"),
            //    //User data
            //    AuthorName = post.User.UserName,
            //    AuthorId = post.User.Id,

            //    //
            //});

            var posts = _postService.GetFilteredPosts(id);

            var model = new ForumTopicModel
            {
                TopicTitle = forum.Title,
                PostList = posts
            };

            return View(model);
        }
    }
}
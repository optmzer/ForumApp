using ForumApp.Data.Models;
using ForumApp.Models.Forum;
using ForumApp.Models.Search;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Controllers
{
    public class SearchController: Controller
    {

        private readonly IPost _postService;

        public SearchController(IPost postService)
        {
            _postService = postService;
        }

        public IActionResult Results(string searchQuery)
        {
            var posts = _postService.GetFilteredPosts(searchQuery);

            var noPostsFund = (!string.IsNullOrEmpty(searchQuery)
                                || !posts.Any());

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

            var model = new SearchResultModel
            {
                Posts = postList,
                SearchQuery = searchQuery,
                EmptySearchResult = noPostsFund
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Search(string search)
        {
            return View();
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

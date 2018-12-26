using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Models.Forum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ForumApp.Controllers
{
    public class ForumController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string AzureBlobStorageConnection;

        private readonly IForum _forumService;
        private readonly IPost _postService;
        private readonly IUpload _uploadService;

        public ForumController(
                          IForum forumService
                        , IPost postService
                        , IUpload uploadService
                        , IConfiguration config)
        {
            _forumService = forumService;
            _postService = postService;
            _uploadService = uploadService;
            _config = config;
            AzureBlobStorageConnection = _config.GetConnectionString("AzureBlobStorageConnection");
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

        public IActionResult CreateNewForum()
        {
            //Passing empty model to the View.
            var model = new AddNewForumModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewForum(AddNewForumModel model)
        {
            var imageUri = "/images/Forum/default.png";

            if(model.ImageUpload != null)
            {
                var blockBlob = UploadForumImage(model.ImageUpload);
                imageUri = blockBlob.Uri.AbsoluteUri;
            }

            var forum = new Forum
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = imageUri
            };

            await _forumService.AddNewForum(forum);

            return RedirectToAction("Index", "Forum");
        }

        private CloudBlockBlob UploadForumImage(IFormFile file)
        {
            var container = _uploadService.GetStorageContainer(AzureBlobStorageConnection);
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var fileName = contentDisposition.FileName.Trim('"');
            var blockBlob = container.GetBlockBlobReference(fileName);
            // Do not need await modifier in this case
            blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            return blockBlob;
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
using Microsoft.AspNetCore.Http;

namespace ForumApp.Models.Forum
{
    public class AddNewForumModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public IFormFile ImageUpload { get; set; }
    }
}

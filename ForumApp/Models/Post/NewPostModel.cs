using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Models.Post
{
    public class NewPostModel
    {
        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public string ForumImageUrl { get; set; }
        public string AuthorName { get; set; }

        public string PostTitle { get; set; }
        public string PostContent { get; set; }
    }
}

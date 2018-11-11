using ForumApp.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Models.Home
{
    public class HomeIndexModel
    {
        public IEnumerable<ForumTopicListingModel> LatestPosts { get; set; }
        public string SearchQuery { get; set; }

    }
}

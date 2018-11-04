using ForumApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Models.Forum
{
    public class ForumTopicModel
    {
        public string TopicTitle { get; set; }
        public IEnumerable<ForumPostListingModel> PostList { set; get; }
    }
}

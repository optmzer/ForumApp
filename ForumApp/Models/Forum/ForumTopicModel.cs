using System.Collections.Generic;

namespace ForumApp.Models.Forum
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<ForumTopicListingModel> PostList { get; set; }
        public string SearchQuery { get; set; } = "";
    }
}

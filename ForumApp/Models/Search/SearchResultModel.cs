using ForumApp.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumApp.Models.Search
{
    public class SearchResultModel
    {
        public IEnumerable<ForumTopicListingModel> Posts { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearchResult { get; set; }
    }
}

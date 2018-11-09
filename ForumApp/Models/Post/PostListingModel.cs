namespace ForumApp.Models.Forum
{
    public class PostListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Created { get; set; }
        
        // Author data
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorId { get; set; }

        // Forum Listing Model - Data about the forum
        public ForumListingModel Forum { get; set; }
       
        // Post replies
        public int RepliesCount { get; set; }
    }
}

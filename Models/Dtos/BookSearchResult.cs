namespace BookWordCount.Models.Dtos
{
    public class BookSearchResult
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
    }
}

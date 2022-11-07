namespace BookWordCount.Models.Dtos
{
    public class BookSearchResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
    }
}

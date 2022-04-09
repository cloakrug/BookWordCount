namespace BookWordCount.Models.Dtos
{
    public class AddBookDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<int> Genres { get; set; }
    }
}

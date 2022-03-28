namespace BookWordCount.Models.Dtos
{
    public class BookGenreDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Genre { get; set; }
    }
}

namespace BookWordCount.Models
{
    public class BookGenre
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Genre { get; set; }
    }
}

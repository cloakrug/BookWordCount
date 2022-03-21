namespace BookWordCount.Models
{
    public class WordCount
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
    }
}

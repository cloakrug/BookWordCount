namespace BookWordCount.Models.Dtos
{
    public class WordCountDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
    }
}

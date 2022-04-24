namespace BookWordCount.Models.Dtos
{
    public class ChangeWordCountDto
    {
        public int WordCountId { get; set; }
        public int BookId { get; set; }
        public int WordCount { get; set; }
    }
}

namespace BookWordCount.Models.Dtos
{
    public class WordCountDto
    {
        public string UserId { get; set; }
        public string BookId { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}

namespace BookWordCount.Models.Dtos
{
    public class WordCountDto
    {
        public int UserId { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public BookDto Book { get; set; }
    }
}

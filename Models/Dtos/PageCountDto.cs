namespace BookWordCount.Models.Dtos
{
    public class PageCountDto
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}

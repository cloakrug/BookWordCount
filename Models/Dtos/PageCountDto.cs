namespace BookWordCount.Models.Dtos
{
    public class PageCountDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
    }
}

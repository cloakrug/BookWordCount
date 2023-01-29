namespace BookWordCount.Models.Dtos
{
    public class DurationDto
    {
        public string UserId { get; set; }
        public string BookId { get; set; }
        public int TimeInSeconds { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}

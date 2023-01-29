using BookWordCount.Models.Database;

namespace BookWordCount.Models.Dtos
{
    public class AddUserBookStatsDto
    {
        public string? UserId { get; set; }
        public string BookId { get; set; }
        public int? wordCount { get; set; }
        public int? pageCount { get; set; }
        public int? difficulty { get; set; }
        public int? durationInSeconds { get; set; }
    }
}
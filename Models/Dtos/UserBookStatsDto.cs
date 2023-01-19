using BookWordCount.Models.Database;

namespace BookWordCount.Models.Dtos
{
    public class UserBookStatsDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int BookId { get; set; }
        public int? wordCount { get; set; }
        public int? pageCount { get; set; }
        public int? difficulty { get; set; }
        public int? durationInSeconds { get; set; }
    }
}
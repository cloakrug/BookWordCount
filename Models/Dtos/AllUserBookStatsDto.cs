namespace BookWordCount.Models.Dtos
{
    public class AllUserBookStatsDto
    {
        public List<WordCountDto> WordCounts { get; set; }
        public List<DurationDto> Duration { get; set; }
        public List<PageCountDto> PageCount { get; set; }
        public List<DifficultyDto> Difficulty { get; set; }
        public List<BookDto> Books { get; set; }
    }
}

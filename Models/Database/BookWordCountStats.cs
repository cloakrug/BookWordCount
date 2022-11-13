using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Database
{
    public class BookWordCountStats
    {
        [Key]
        public int BookId { get; set; }
        public double AverageWordCount { get; set; }
        public int TotalCountsSubmitted { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Database
{
    public class PageCount
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
    }
}

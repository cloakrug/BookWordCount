using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Database
{
    public class BookGenre
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Genre { get; set; }
    }
}

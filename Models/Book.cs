using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        
        virtual public List<BookGenre> Genres { get; set; }
    }
}

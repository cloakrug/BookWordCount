using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Database
{
    public class Book
    {
        [Key] // Don't really need this because Id is made the PK by convention
        public string Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        virtual public List<Genre> Genres { get; set; }
        virtual public MajorGenre MajorGenre { get; set; }
    }
}

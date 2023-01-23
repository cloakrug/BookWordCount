using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Dtos
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        virtual public List<GenreDto> Genres { get; set; }
        virtual public MajorGenreDto MajorGenre { get; set; }
    }
}

using BookWordCount.Models.Dtos;

namespace BookWordCount.Interfaces
{
    public interface IGenreService
    {
        IEnumerable<GenreDto> GetGenres();
        GenreDto GetGenre(int id);
    }
}

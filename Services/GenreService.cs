using BookWordCount.Interfaces;
using BookWordCount.Models;
using BookWordCount.Models.Dtos;
using AutoMapper;

namespace BookWordCount.Services
{
    public class GenreService : IGenreService
    {
        private readonly BookContext _bookContext;
        private readonly IMapper _mapper;

        public GenreService(BookContext bookContext, IMapper mapper)
        {
            _bookContext = bookContext;
            _mapper = mapper;
        }

        public IEnumerable<GenreDto> GetGenres()
        {
            var genres = _bookContext.Genres;
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public GenreDto GetGenre(int id)
        {
            var genre = _bookContext.Genres.Find(id);
            return _mapper.Map<GenreDto>(genre);
        }
    }
}

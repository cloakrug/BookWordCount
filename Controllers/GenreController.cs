using BookWordCount.Interfaces;
using BookWordCount.Models.Dtos;
using AutoMapper;
using BookWordCount.Models;
using BookWordCount.Models.Database;
using BookWordCount.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookWordCount.Controllers
{
    [Route("Genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet("", Name = "GetGenres")]
        public IActionResult Get()
        {
            var genres = _genreService.GetGenres();

            return Ok(_mapper.Map<List<GenreDto>>(genres));
        }

        [HttpGet("{id}", Name = "GetGenreById")]
        public IActionResult Get(int id)
        {
            var genre = _genreService.GetGenre(id);

            if (genre == null) return NotFound();

            return Ok(_mapper.Map<GenreDto>(genre));
        }


        //[HttpPost("Add", Name = nameof(Add))]
        //public IActionResult Add(AddBookDto addBookDto)
        //{
        //    return Ok(_bookService.AddBook(addBookDto));
        //}


        //[HttpPatch("Update/{id}", Name = nameof(Update))]
        //public IActionResult Update(BookDto bookDto)
        //{
        //    var book = _mapper.Map<Book>(bookDto);

        //    return Ok(_bookService.UpdateBook(book));
        //}

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    return Ok(_bookService.DeleteBook(id));
        //}

    }
}

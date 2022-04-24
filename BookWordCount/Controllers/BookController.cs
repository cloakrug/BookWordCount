using AutoMapper;
using BookWordCount.Interfaces;
using BookWordCount.Models;
using BookWordCount.Models.Database;
using BookWordCount.Models.Dtos;
using BookWordCount.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookWordCount.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet("", Name = "GetBooks")]
        public IActionResult Get()
        {
            var books = _bookService.GetBooks();

            return Ok(_mapper.Map<List<BookDto>>(books));
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public IActionResult Get(int id)
        {
            var book = _bookService.GetBook(id);

            if (book == null) return NotFound();

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPost("Add", Name = nameof(Add))]
        public IActionResult Add(AddBookDto addBookDto)
        {
            return Ok(_bookService.AddBook(addBookDto));
        }

        [HttpPost("Update/{id}", Name = nameof(Update))]
        public IActionResult Update(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);

            return Ok(_bookService.UpdateBook(book));
        }

        [HttpDelete("Delete/{id}", Name = nameof(Delete))]
        public IActionResult Delete(int id)
        {
            return Ok(_bookService.DeleteBook(id));
        }

    }
}

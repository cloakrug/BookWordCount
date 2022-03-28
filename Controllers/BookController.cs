using BookWordCount.Models;
using BookWordCount.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookWordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("Get", Name = nameof(Get))]

        [HttpGet("", Name = nameof(Get))]
        public IActionResult Get()
        {
            // TODO: set up mapper
            return Ok(_bookService.GetBooks());
        }

        [HttpGet("/{id}", Name = "GetBookById")]
        public IActionResult Get(int id)
        {
            // TODO: set up mapper
            return Ok(_bookService.GetBook(id));
        }

        [HttpPost("Add", Name = nameof(Add))]
        public IActionResult Add(Book book)
        {
            // TODO: set up mapper
            return Ok(_bookService.AddBook(book));
        }


        [HttpPatch("Update/{id}", Name = nameof(Update))]
        public IActionResult Update(Book book)
        {
            // TODO: set up mapper
            return Ok(_bookService.UpdateBook(book));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok(_bookService.DeleteBook(id));
        }

    }
}

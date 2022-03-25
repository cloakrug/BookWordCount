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

        [HttpGet("GetBooks", Name = nameof(GetBooks))]
        public IActionResult GetBooks()
        {
            // TODO: set up mapper
            return Ok(_bookService.GetBooks());
        }
    }
}

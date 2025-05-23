﻿using BookWordCount.Interfaces;
using BookWordCount.Models.Database;
using BookWordCount.Models.Dtos;
using AutoMapper;
using BookWordCount.Models;
using BookWordCount.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Get(string id)
        {
            var book = _bookService.GetBook(id);

            if (book == null) return NotFound();

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpGet("search", Name = "Search")]
        public IActionResult Search(string str, int? pageNum)
        {
            var results = _bookService.Search(str, Constants.SearchMethods.Popularity, 10, pageNum);

            IEnumerable<BookSearchResult> searchResults =
                results.Select<Book, BookSearchResult>( book =>
                    {
                        return new BookSearchResult()
                        {
                            Id = book.Id,
                            Title = book.Title,
                            Author = book.Author,
                            ImageUrl = book.ImageUrl,
                            ReleaseDate = book.ReleaseDate,
                            ShortDescription = new string(book.Description.Take(200).ToArray())
                    };
                });

            return Ok(searchResults.ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add", Name = nameof(Add))]
        public IActionResult Add(AddBookDto addBookDto)
        {
            return Ok(_bookService.AddBook(addBookDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}", Name = nameof(Update))]
        public IActionResult Update(string id, [FromBody]UpdateBookDto bookDto)
        {
            return Ok(_bookService.UpdateBook(id, bookDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}", Name = nameof(Delete))]
        public IActionResult Delete(string id)
        {
            return Ok(_bookService.DeleteBook(id));
        }

    }
}

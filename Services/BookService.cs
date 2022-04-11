using AutoMapper;
using BookWordCount.Constants;
using BookWordCount.Interfaces;
using BookWordCount.Models;
using BookWordCount.Models.Database;
using BookWordCount.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookWordCount.Services
{
    public class BookService : IBookService
    {
        private readonly BookContext _ctx;
        private readonly IMapper _mapper;

        public BookService(BookContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _ctx.Books.Include(x => x.Genres);
        }

        public Book? GetBook(int id)
        {
            return _ctx.Books.Find(id);
        }

        //// TODO: pagination
        //public IEnumerable<Book> Search(string term, SearchMethods method = SearchMethods.Popularity)
        //{

        //}

        //public IEnumerable<Book> GetBooksByGenre()
        //{
        //}

        public BookDto AddBook(AddBookDto addBookDto)
        {
            var book = new Book();
            book.Title = addBookDto.Title;
            book.Description = addBookDto.Description;
            book.ReleaseDate = addBookDto.ReleaseDate;
            book.ImageUrl = addBookDto.ImageUrl ?? "";
            book.Genres = new List<Genre>();

            addBookDto.Genres = addBookDto.Genres.Distinct().ToList();

            addBookDto.Genres.ForEach(genreToAdd =>
            {
                var genre = _ctx.Genres.Find(genreToAdd);
                if (genre == null) return;
                book.Genres.Add(genre);
            });

            _ctx.Books.Add(book);

            _ctx.SaveChanges();

            return _mapper.Map<BookDto>(book);
        }

        public Book UpdateBook(Book book)
        {
            _ctx.Update(book);
            _ctx.SaveChanges();
            return book;
        }

        public bool DeleteBook(int id)
        {
            var entity = _ctx.Books.Find(id);

            if (entity == null) return false;

            _ctx.Books.Remove(entity);
            _ctx.SaveChanges();
            return true;
        }
    }
}

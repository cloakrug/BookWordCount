using BookWordCount.Constants;
using BookWordCount.Interfaces;
using BookWordCount.Models;
using BookWordCount.Models.Database;
using BookWordCount.Models.Dtos;

namespace BookWordCount.Services
{
    public class BookService : IBookService
    {
        private readonly BookContext _ctx;

        public BookService(BookContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _ctx.Books;
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

        public Book AddBook(AddBookDto addBookDto)
        {
            var book = new Book();
            book.Title = addBookDto.Title;
            book.Description = addBookDto.Description;
            book.ReleaseDate = addBookDto.ReleaseDate;
            book.ImageUrl = addBookDto.ImageUrl ?? "";

            _ctx.Books.Add(book);
            _ctx.SaveChanges();

            addBookDto.Genres.ForEach(genreToAdd =>
            {
                var genre = _ctx.Genres.Find(genreToAdd);
                if (genre == null) return;

                var bookGenre = new BookGenre();
                bookGenre.BookId = book.Id;
                bookGenre.Genre = genre;

                _ctx.BookGenre.Add(bookGenre);
            });

            _ctx.SaveChanges();

            return book;
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

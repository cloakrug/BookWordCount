using BookWordCount.Models;

namespace BookWordCount.Services
{
    public class BookService
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

        //public IEnumerable<Book> GetBooksByGenre()
        //{
        //}

        public Book AddBook(Book book)
        {
            _ctx.Books.Add(book);
            _ctx.SaveChanges();
            return book;
        }

        public Book UpdateBook(Book book)
        {
            _ctx.Update(book);
            _ctx.SaveChanges();
            return book;
        }

        public void RemoveBook(Book book)
        {
            _ctx.Books.Remove(book);
            _ctx.SaveChanges();
        }
    }
}

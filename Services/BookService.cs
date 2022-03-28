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

        public Book? GetBook(int id)
        {
            return _ctx.Books.Find(id);
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

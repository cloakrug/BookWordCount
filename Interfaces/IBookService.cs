using BookWordCount.Models.Database;
using BookWordCount.Models.Dtos;

namespace BookWordCount.Interfaces
{
    public interface IBookService
    {
        BookDto AddBook(AddBookDto addBookDto);
        bool DeleteBook(int id);
        Book? GetBook(int id);
        IEnumerable<Book> GetBooks();
        Book UpdateBook(Book book);
    }
}

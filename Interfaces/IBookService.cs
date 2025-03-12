using BookWordCount.Constants;
using BookWordCount.Models.Database;
using BookWordCount.Models.Dtos;

namespace BookWordCount.Interfaces
{
    public interface IBookService
    {
        BookDto AddBook(AddBookDto addBookDto);
        bool DeleteBook(string id);
        Book? GetBook(string id);
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> Search(string searchTerm, SearchMethods method = SearchMethods.Popularity, int? pageSize = 10, int? pageNumber = null, string? filter = null);
        Book UpdateBook(string id, UpdateBookDto book);
    }
}

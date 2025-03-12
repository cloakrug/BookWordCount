using BookWordCount.Interfaces;
using BookWordCount.Models;
using BookWordCount.Models.Database;
using BookWordCount.Models.Dtos;
using AutoMapper;
using BookWordCount.Constants;
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
            return _ctx.Books
                .Include(x => x.Genres)
                .Include(x => x.MajorGenre);
        }

        public Book? GetBook(string id)
        {
            return _ctx.Books
                .Include(x => x.Genres)
                .Include(x => x.MajorGenre)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Book> Search(
            string searchTerm, 
            SearchMethods method = SearchMethods.Popularity,
            int? pageSize = 10,
            int? pageNumber = null,
            string? filter = null
            )
        {
            var res = GetBooks()
                .Where(book => book.Title.ToLower().Contains(searchTerm.ToLower()));

            if (filter != null)
            {
                res = res.Where(book => book.Title != filter);
            }

            res = res.OrderBy(book => book.Title);   // TODO: sort by popularity

            if (pageNumber != null && pageSize != null)
            {
                res = res.Skip(pageNumber.Value * pageSize.Value);
                res = res.Take(pageSize.Value);
            }

            return res;
        }

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

        public Book UpdateBook(string id, UpdateBookDto book)
        {
            var curr = _ctx.Books.FirstOrDefault(book => book.Id == id);

            if (book == null ) return null;

            curr.Title = book.Title;
            curr.Description = book.Description;

            _ctx.Update(curr);
            _ctx.SaveChanges();
            return curr;
        }

        public bool DeleteBook(string id)
        {
            var entity = _ctx.Books.Find(id);

            if (entity == null) return false;

            _ctx.Books.Remove(entity);
            _ctx.SaveChanges();
            return true;
        }
    }
}

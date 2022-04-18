using BookWordCount.Models;
using BookWordCount.Models.Database;

namespace BookWordCount.Helpers
{
    public class BookDbInitializer
    {
        private readonly BookContext _context;

        public BookDbInitializer(BookContext context)
        {
            _context = context;
        }

        public void SeedDatabase()
        {
            _context.AddRange(getDefaultGenres());
            _context.SaveChanges();

            _context.AddRange(getDefaultMajorGenres());
            _context.SaveChanges();

            _context.AddRange(getDefaultBooks());
            _context.SaveChanges();
        }

        private IList<Genre> getDefaultGenres()
        {
            IList<Genre> defaultGenres = new List<Genre>();

            defaultGenres.Add(new Genre() { Id = 1, Name = "Action" });
            defaultGenres.Add(new Genre() { Id = 2, Name = "Horror" });
            defaultGenres.Add(new Genre() { Id = 3, Name = "Mystery" });

            return defaultGenres;
        }

        private IList<MajorGenre> getDefaultMajorGenres()
        {
            List<MajorGenre> defaultMajorGenres = new List<MajorGenre>();

            defaultMajorGenres.Add(new MajorGenre() { Id = 1, Name = "Fiction" });
            defaultMajorGenres.Add(new MajorGenre() { Id = 2, Name = "Non-Fiction" });

            return defaultMajorGenres;
        }

        public IList<Book> getDefaultBooks()
        {
            IList<Book> defaultBooks = new List<Book>();

            defaultBooks.Add(new Book()
            {
                Id = 1,
                Title = "The Road",
                CreatedDate = DateTime.Now,
                ReleaseDate = DateTime.Now,
                Description = "depressing...",
                ImageUrl = "",
                Genres = new List<Genre>() { _context.Genres.ToList()[0] },
                MajorGenre = _context.MajorGenres.ToList()[0]
            });

            defaultBooks.Add(new Book()
            {
                Id = 2,
                Title = "Kite Runner",
                CreatedDate = DateTime.Now,
                ReleaseDate = DateTime.Now,
                Description = "nice book",
                ImageUrl = "",
                Genres = new List<Genre>() { _context.Genres.ToList()[1] },
                MajorGenre = _context.MajorGenres.ToList()[1]
            });

            return defaultBooks;
        }
    }
}

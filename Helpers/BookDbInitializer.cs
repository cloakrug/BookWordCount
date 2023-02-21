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

            _context.AddRange(getDefaultWordCounts());
            _context.SaveChanges();

            _context.AddRange(getDefaultDurations());
            _context.SaveChanges();

            _context.AddRange(getDefaultDifficultys());
            _context.SaveChanges();
        }

        private IList<Genre> getDefaultGenres()
        {
            IList<Genre> defaultGenres = new List<Genre>();

            defaultGenres.Add(new Genre() { Id = 1, Name = "Action" });
            defaultGenres.Add(new Genre() { Id = 2, Name = "Horror" });
            defaultGenres.Add(new Genre() { Id = 3, Name = "Historical" });
            defaultGenres.Add(new Genre() { Id = 4, Name = "Romance" });
            defaultGenres.Add(new Genre() { Id = 5, Name = "Western" });
            defaultGenres.Add(new Genre() { Id = 6, Name = "Science Fiction" });
            defaultGenres.Add(new Genre() { Id = 7, Name = "Fantasy" });
            defaultGenres.Add(new Genre() { Id = 8, Name = "Adventure" });
            defaultGenres.Add(new Genre() { Id = 9, Name = "Dystopian" });
            defaultGenres.Add(new Genre() { Id = 10, Name = "Mystery" });

            return defaultGenres;
        }

        private IList<MajorGenre> getDefaultMajorGenres()
        {
            List<MajorGenre> defaultMajorGenres = new List<MajorGenre>();

            defaultMajorGenres.Add(new MajorGenre() { Id = 1, Text = "Fiction" });
            defaultMajorGenres.Add(new MajorGenre() { Id = 2, Text = "Non-Fiction" });

            return defaultMajorGenres;
        }

        public IList<Book> getDefaultBooks()
        {
            IList<Book> defaultBooks = new List<Book>();

            defaultBooks.Add(new Book()
            {
                Id = "1",
                Author = "Cormac McCarthy",
                Title = "The Road",
                CreatedDate = DateTime.Now,
                ReleaseDate = DateTime.Now,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                ImageUrl = "",
                Genres = _context.Genres.ToList(),
                MajorGenre = _context.MajorGenres.ToList()[0]
            });

            defaultBooks.Add(new Book()
            {
                Id = "2",
                Author = "Khaled Hosseini",
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

        private IList<WordCount> getDefaultWordCounts()
        {
            List<WordCount> defaultWordCounts = new List<WordCount>();

            defaultWordCounts.Add(new WordCount()
            {
                Book = _context.Books.ToList()[0],
                UserId = "1",
                Count = 23,
                Created = DateTime.Now,
                LastModified = DateTime.Now
            });

            defaultWordCounts.Add(new WordCount()
            {
                Book = _context.Books.ToList()[1],
                UserId = "1",
                Count = 44,
                Created = DateTime.Now,
                LastModified = DateTime.Now
            });

            return defaultWordCounts;
        }

        private IList<Duration> getDefaultDurations()
        {
            List<Duration> defaultDurations = new List<Duration>();

            defaultDurations.Add(new Duration()
            {
                Book = _context.Books.ToList()[0],
                UserId = "1",
                TimeInSeconds = 5401,
                Created = DateTime.Now,
                LastModified = DateTime.Now,
            });

            defaultDurations.Add(new Duration()
            {
                Book = _context.Books.ToList()[1],
                UserId = "1",
                TimeInSeconds = 65,
                Created = DateTime.Now,
                LastModified = DateTime.Now
            });

            return defaultDurations;
        }

        private IList<Difficulty> getDefaultDifficultys()
        {
            List<Difficulty> defaultDifficultys = new List<Difficulty>();

            defaultDifficultys.Add(new Difficulty()
            {
                Book = _context.Books.ToList()[1],
                UserId = "1",
                DifficultyOfBook = 7,
                Created = DateTime.Now,
                LastModified = DateTime.Now
            });

            return defaultDifficultys;
        }
    }
}

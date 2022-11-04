using BookWordCount.Models.Database;
using BookWordCount.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BookWordCount.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<MajorGenre> MajorGenres { get; set; } = null!;
        public DbSet<PageCount> PageCounts { get; set; } = null!;
        public DbSet<WordCount> WordCounts { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}

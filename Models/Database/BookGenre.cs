using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWordCount.Models.Database
{
    public class BookGenre
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }

        virtual public Genre Genre { get; set; }
    }
}

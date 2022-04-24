using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Database
{
    public class WordCount
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        virtual public Book Book { get; set; }
    }
}

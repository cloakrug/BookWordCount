using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Database
{
    public class WordCount
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public int BookId{ get; set; }
        virtual public Book Book { get; set; }
    }
}

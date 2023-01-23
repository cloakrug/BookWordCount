using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Database
{
    public class PageCount
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string BookId { get; set; }
        virtual public Book Book { get; set; }
    }
}

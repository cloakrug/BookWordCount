using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Database
{
    public class Duration
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TimeInSeconds { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        virtual public Book Book { get; set; }
    }
}

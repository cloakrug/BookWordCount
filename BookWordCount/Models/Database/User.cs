using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Database
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Username { get; set; }
    }
}

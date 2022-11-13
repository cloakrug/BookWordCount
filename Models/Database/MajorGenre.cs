using System.ComponentModel.DataAnnotations;

namespace BookWordCount.Models.Database
{
    public class MajorGenre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

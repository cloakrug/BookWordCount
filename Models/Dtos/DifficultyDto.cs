namespace BookWordCount.Models.Dtos
{
    public class DifficultyDto
    {
        public string UserId { get; set; }
        public int Time { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}

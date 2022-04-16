using BookWordCount.Models.Dtos;

namespace BookWordCount.Interfaces
{
    public interface IWordCountService
    {
        bool AddWordCount(ChangeWordCountDto changeWordCountDto, int userId);
        bool DeleteWordCount(int wordCountId);
        WordCountDto GetUserWordCount(int userId, int bookId);
        IEnumerable<WordCountDto> GetUserWordCounts(int userId);
        bool UpdateWordCount(ChangeWordCountDto changeWordCountDto, int userId);
    }
}

using BookWordCount.Models.Dtos;

namespace BookWordCount.Interfaces
{
    public interface IUserBookStatService
    {
        AddUserBookStatsDto AddUserBookStats(AddUserBookStatsDto userBookStats);
        bool DeleteStatsForBook(string wordCountId, string userId);
        IEnumerable<UserBookStatsDto> GetAllUserBookStats(string userId);
        AddUserBookStatsDto GetUserBookStats(string userId, string bookId);
    }
}

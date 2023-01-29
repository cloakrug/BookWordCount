using BookWordCount.Models.Dtos;

namespace BookWordCount.Interfaces
{
    public interface IUserBookStatService
    {
        AddUserBookStatsDto AddUserBookStats(AddUserBookStatsDto userBookStats);
        IEnumerable<UserBookStatsDto> GetAllUserBookStats(string userId);
        AddUserBookStatsDto GetUserBookStats(string userId, string bookId);
    }
}

using BookWordCount.Models.Dtos;

namespace BookWordCount.Interfaces
{
    public interface IUserBookStatService
    {
        UserBookStatsDto AddUserBookStats(UserBookStatsDto userBookStats);
        AllUserBookStatsDto GetAllUserBookStats(string userId);
        UserBookStatsDto GetUserBookStats(string userId, int bookId);
    }
}

using BookWordCount.Interfaces;
using BookWordCount.Models;
using BookWordCount.Models.Dtos;
using AutoMapper;
using BookWordCount.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace BookWordCount.Services
{


    public class UserBookStatService : IUserBookStatService
    {

        private readonly BookContext _ctx;
        private readonly IMapper _mapper;

        public UserBookStatService(BookContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public IEnumerable<UserBookStatsDto> GetAllUserBookStats(string userId)
        {
            var wordCounts = _ctx.WordCounts
                .Where(count => count.UserId == userId);

            var pageCounts = _ctx.PageCounts
                .Where(count => count.UserId == userId);

            var durations = _ctx.Durations
                .Where(duration => duration.UserId == userId);

            var difficultys = _ctx.DifficultyStats
                .Where(difficulty => difficulty.UserId == userId);

            var books = wordCounts.Select(wc => wc.Book)
                         .Union(pageCounts.Select(pc => pc.Book))
                         .Union(difficultys.Select(d => d.Book))
                         .Union(durations.Select(d => d.Book))
                         .Distinct()
                         .ToList(); // Get a list of all the books in any of the stats

            var wordCountDtos = _mapper.Map<IEnumerable<WordCountDto>>(wordCounts);
            var pageCountDtos = _mapper.Map<IEnumerable<PageCountDto>>(pageCounts);
            var durationDtos = _mapper.Map<IEnumerable<DurationDto>>(durations);
            var difficultyDtos = _mapper.Map<IEnumerable<DifficultyDto>>(difficultys);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);

            var res = from book in bookDtos
                      join wc in wordCountDtos on book.Id equals wc.BookId into wcg
                      from subWc in wcg.DefaultIfEmpty()
                      join pc in pageCountDtos on book.Id equals pc.BookId into pcg
                      from subPc in pcg.DefaultIfEmpty()
                      join dur in durationDtos on book.Id equals dur.BookId into durg
                      from subDur in durg.DefaultIfEmpty()
                      join diff in difficultyDtos on book.Id equals diff.BookId into difg
                      from subDif in difg.DefaultIfEmpty()
                      select new UserBookStatsDto()
                      {
                          book = book,
                          wordCount = subWc?.Count,
                          pageCount = subPc?.Count,
                          durationInSeconds = subDur?.TimeInSeconds,
                          difficulty = subDif?.DifficultyOfBook
                      };

            return res;
        }

        public AddUserBookStatsDto AddUserBookStats(AddUserBookStatsDto userBookStats)
        {
            WordCount wordCounts;
            PageCount pageCounts;
            Difficulty difficulty;
            Duration duration;

            if (userBookStats.wordCount != null)
            {   
                wordCounts = _mapper.Map<WordCount>(userBookStats);
                _ctx.WordCounts.Add(wordCounts);
            }

            if (userBookStats.pageCount != null)
            {
                pageCounts = _mapper.Map<PageCount>(userBookStats);
                _ctx.PageCounts.Add(pageCounts);
            }

            if (userBookStats.difficulty != null)
            {
                difficulty = _mapper.Map<Difficulty>(userBookStats);
                _ctx.DifficultyStats.Add(difficulty);
            }

            if (userBookStats.durationInSeconds != null)
            {
                duration = _mapper.Map<Duration>(userBookStats);
                _ctx.Durations.Add(duration);
            }

            _ctx.SaveChanges();   

            return userBookStats;
        }

        public AddUserBookStatsDto GetUserBookStats(string userId, string bookId)
        {
            var wordCount = _ctx.WordCounts
                .Where(count => count.UserId == userId && count.Book.Id == bookId)
                .Take(1);

            var pageCount = _ctx.PageCounts
                .Where(count => count.UserId == userId && count.Book.Id == bookId)
                .Take(1);

            var duration = _ctx.Durations
                .Where(duration => duration.UserId == userId && duration.Book.Id == bookId)
                .Take(1);

            var difficulty = _ctx.DifficultyStats
                .Where(difficulty => difficulty.UserId == userId && difficulty.Book.Id == bookId)
                .Take(1);

            var userBookStats = new AddUserBookStatsDto()
            {
                wordCount = wordCount.FirstOrDefault()?.Count,
                pageCount = pageCount.FirstOrDefault()?.Count,
                durationInSeconds = duration.FirstOrDefault()?.TimeInSeconds,
                difficulty = difficulty.FirstOrDefault()?.DifficultyOfBook
            };

            return userBookStats;
        }

        //public WordCountDto GetUserWordCount(string $2, int bookId)
        //{
        //    var wordCount = _ctx.WordCounts.Where(count =>
        //        count.UserId == userId &&
        //        count.Book.Id == bookId
        //    ).FirstOrDefault();

        //    return _mapper.Map<WordCountDto>(wordCount);
        //}

        //public bool AddWordCount(ChangeWordCountDto changeWordCountDto, string userId)
        //{
        //    var book = _ctx.Books.Find(changeWordCountDto.BookId);

        //    if (book == null) return false;

        //    var wordCount = new WordCount()
        //    {
        //        Book = book,
        //        UserId = userId,
        //        Count = changeWordCountDto.WordCount,
        //        Created = DateTime.Now,
        //        LastModified = DateTime.Now
        //    };

        //    _ctx.WordCounts.Add(wordCount);
        //    var res = _ctx.SaveChanges();
        //    return res > 0;
        //}

        //public bool UpdateWordCount(ChangeWordCountDto changeWordCountDto, string userId)
        //{
        //    var wordCount = _ctx.WordCounts.Where(count =>
        //        count.Id == changeWordCountDto.WordCountId &&
        //        count.UserId == userId
        //    ).FirstOrDefault();

        //    if (wordCount == null) return false;

        //    wordCount.Count = changeWordCountDto.WordCount;
        //    wordCount.LastModified = DateTime.Now;

        //    _ctx.SaveChanges();
        //    return true;
        //}

        public bool DeleteStatsForBook(string bookId, string userId)
        {
            var wc = _ctx.WordCounts
                .Where(count => count.Book.Id == bookId )
                .Where(count => count.UserId == userId).FirstOrDefault();

            var pc = _ctx.PageCounts
                .Where(count => count.Book.Id == bookId)
                .Where(count => count.UserId == userId).FirstOrDefault();

            var dur = _ctx.Durations
                .Where(duration => duration.Book.Id == bookId)
                .Where(duration => duration.UserId == userId).FirstOrDefault();
            
            var dif = _ctx.DifficultyStats
                .Where(difficulty => difficulty.Book.Id == bookId)
                .Where(difficulty => difficulty.UserId == userId).FirstOrDefault();

            if (wc != null)
            {
                _ctx.WordCounts.Remove(wc);
            }

            if (pc != null)
            {
                _ctx.PageCounts.Remove(pc);
            }

            if (dur != null)
            {
                _ctx.Durations.Remove(dur);
            }

            if (dif != null)
            {
                _ctx.DifficultyStats.Remove(dif);
            }

            _ctx.SaveChanges();

            return true;
        }

    }
}

using AutoMapper;
using BookWordCount.Interfaces;
using BookWordCount.Models;
using BookWordCount.Models.Database;
using BookWordCount.Models.Dtos;

namespace BookWordCount.Services
{
    public class WordCountService : IWordCountService
    {

        private readonly BookContext _ctx;
        private readonly IMapper _mapper;

        public WordCountService(BookContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public IEnumerable<WordCountDto> GetUserWordCounts(int userId)
        {
            var wordCounts = _ctx.WordCounts.Where(count => count.UserId == userId);
            return _mapper.Map<IEnumerable<WordCountDto>>(wordCounts);
        }

        public WordCountDto GetUserWordCount(int userId, int bookId)
        {
            var wordCount = _ctx.WordCounts.Where(count =>
                count.UserId == userId &&
                count.BookId == bookId
            ).FirstOrDefault();

            return _mapper.Map<WordCountDto>(wordCount);
        }

        public bool AddWordCount(ChangeWordCountDto changeWordCountDto, int userId)
        {
            var wordCount = new WordCount()
            {
                BookId = changeWordCountDto.BookId,
                UserId = userId,
                Count = changeWordCountDto.WordCount
            };

            _ctx.WordCounts.Add(wordCount);
            var res = _ctx.SaveChanges();
            return res > 0;
        }

        public bool UpdateWordCount(ChangeWordCountDto changeWordCountDto, int userId)
        {
            var wordCount = _ctx.WordCounts.Where(count =>
                count.BookId == changeWordCountDto.BookId &&
                count.UserId == userId
            ).FirstOrDefault();

            if (wordCount == null) return false;

            wordCount.Count = changeWordCountDto.WordCount;
            _ctx.SaveChanges();
            return true;
        }

        public bool DeleteWordCount(int wordCountId)
        {
            var entity = _ctx.WordCounts.Find(wordCountId);

            if (entity == null) return false;

            _ctx.WordCounts.Remove(entity);
            _ctx.SaveChanges();
            return true;
        }

    }
}

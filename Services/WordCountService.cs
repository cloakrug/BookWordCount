using AutoMapper;
using BookWordCount.Interfaces;
using BookWordCount.Models;
using BookWordCount.Models.Database;
using BookWordCount.Models.Dtos;
using Microsoft.EntityFrameworkCore;

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
            var wordCounts = _ctx.WordCounts
                .Where(count => count.UserId == userId)
                .Include(x => x.Book)
                .Include(x => x.Book.Genres)
                .Include(x => x.Book.MajorGenre);
            return _mapper.Map<IEnumerable<WordCountDto>>(wordCounts);
        }

        public WordCountDto GetUserWordCount(int userId, int bookId)
        {
            var wordCount = _ctx.WordCounts.Where(count =>
                count.UserId == userId &&
                count.Book.Id == bookId
            ).FirstOrDefault();

            return _mapper.Map<WordCountDto>(wordCount);
        }

        public bool AddWordCount(ChangeWordCountDto changeWordCountDto, int userId)
        {
            var book = _ctx.Books.Find(changeWordCountDto.BookId);

            if (book == null) return false;

            var wordCount = new WordCount()
            {
                Book = book,
                UserId = userId,
                Count = changeWordCountDto.WordCount,
                Created = DateTime.Now,
                LastModified = DateTime.Now
            };

            _ctx.WordCounts.Add(wordCount);
            var res = _ctx.SaveChanges();
            return res > 0;
        }

        public bool UpdateWordCount(ChangeWordCountDto changeWordCountDto, int userId)
        {
            var wordCount = _ctx.WordCounts.Where(count =>
                count.Id == changeWordCountDto.WordCountId &&
                count.UserId == userId
            ).FirstOrDefault();

            if (wordCount == null) return false;

            wordCount.Count = changeWordCountDto.WordCount;
            wordCount.LastModified = DateTime.Now;

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

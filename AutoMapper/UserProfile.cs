using BookWordCount.Models.Database;
using BookWordCount.Models.Dtos;
using AutoMapper;

namespace BookWordCount.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<BookDto, Book>().ReverseMap();

            CreateMap<AddBookDto, Book>().ReverseMap();

            CreateMap<GenreDto, Genre>().ReverseMap();

            CreateMap<MajorGenreDto, MajorGenre>().ReverseMap();

            CreateMap<WordCountDto, WordCount>().ReverseMap();

            CreateMap<UserBookStatsDto, WordCount>()
                .ForMember(d => d.Count,
                    opt => opt.MapFrom(src => src.wordCount)
                ).ReverseMap();

            CreateMap<UserBookStatsDto, PageCount>()
                .ForMember(d => d.Count,
                    opt => opt.MapFrom(src => src.pageCount)
                ).ReverseMap();

            CreateMap<UserBookStatsDto, Difficulty>()
                .ForMember(d => d.DifficultyOfBook,
                    opt => opt.MapFrom(src => src.difficulty)
                ).ReverseMap();

            CreateMap<UserBookStatsDto, Duration>()
                .ForMember(d => d.TimeInSeconds,
                    opt => opt.MapFrom(src => src.durationInSeconds)
                ).ReverseMap();
        }
    }
}

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
            CreateMap<PageCountDto, PageCount>().ReverseMap();
            CreateMap<DurationDto, Duration>().ReverseMap();
            CreateMap<DifficultyDto, Difficulty>().ReverseMap();

            CreateMap<AddUserBookStatsDto, WordCount>()
                .ForMember(d => d.Count,
                    opt => opt.MapFrom(src => src.wordCount)
                ).ReverseMap();

            CreateMap<AddUserBookStatsDto, PageCount>()
                .ForMember(d => d.Count,
                    opt => opt.MapFrom(src => src.pageCount)
                ).ReverseMap();

            CreateMap<AddUserBookStatsDto, Difficulty>()
                .ForMember(d => d.DifficultyOfBook,
                    opt => opt.MapFrom(src => src.difficulty)
                ).ReverseMap();

            CreateMap<AddUserBookStatsDto, Duration>()
                .ForMember(d => d.TimeInSeconds,
                    opt => opt.MapFrom(src => src.durationInSeconds)
                ).ReverseMap();
        }
    }
}

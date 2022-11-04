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
        }
    }
}

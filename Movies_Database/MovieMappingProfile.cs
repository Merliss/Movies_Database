using AutoMapper;
using Movies_Database.Entities;
using Movies_Database.Models;

namespace Movies_Database
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(m => m.DirectorName, c => c.MapFrom(s => s.Director.Name))
                .ForMember(m => m.DirectorSurname, c => c.MapFrom(s => s.Director.Surname))
                .ForMember(m => m.Country, c => c.MapFrom(s => s.Country.Name))
                .ForMember(m => m.Genre, c => c.MapFrom(s => s.Genre.Name));

            CreateMap<MovieRating, MovieRatingDto>();
        }


    }
}

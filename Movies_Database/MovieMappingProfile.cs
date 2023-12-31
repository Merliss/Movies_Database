﻿using AutoMapper;
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
                .ForMember(m => m.Genre, c => c.MapFrom(s => s.Genre.Name))
                .ForMember(m => m.MovieRatings, c => c.MapFrom(s => s.MovieRatings));

            CreateMap<MovieRating, MovieRatingDto>()
                .ForMember(m => m.MovieName, c => c.MapFrom(s => s.Movie.Name))
                .ForMember(m => m.UsersId, c => c.MapFrom(s => s.Users.Id))
                .ForMember(m => m.UserName, c => c.MapFrom(s => s.Users.Username));

            CreateMap<Users, UserDto>();

            CreateMap<CreateMovieDto, Movie>()
                .ForMember(m => m.Director, c => c.MapFrom(dto => new Director() { Name = dto.DirectorName, Surname = dto.DirectorSurname }))
                .ForMember(m => m.Genre, c => c.MapFrom(dto => new Genre() { Name = dto.Genre }))
                .ForMember(m => m.Country, c => c.MapFrom(dto => new Country() { Name = dto.Country}));

            CreateMap<CreateMovieDto, Movie>();

            CreateMap<RegisterUserDto, UserDto>();

            CreateMap<CreateMovieRatingDto, MovieRating>();
        }       



    }
}

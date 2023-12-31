﻿using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies_Database.Entities;
using Movies_Database.Exceptions;
using Movies_Database.Models;

namespace Movies_Database.Services
{

    public interface IMovieService
    {
        MovieDto GetMovieById(int id);
        PagedResult<MovieDto> GetAllMovies(MovieQuery query);
        int Create(CreateMovieDto dto);
        void Update(UpdateMovieDto dto, int id);

        bool Delete(int id);
    }
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<MovieService> _logger;
        public MovieService(MovieDbContext dbContext, IMapper mapper, ILogger<MovieService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public MovieDto GetMovieById (int id)
        {
            var movie = _dbContext
                .Movies
                .Include(m => m.Director)
                .Include(m => m.Country)
                .Include(m => m.Genre)
                .Include(m => m.MovieRatings)
                .FirstOrDefault(x => x.Id == id);

            if(movie == null)
            {
                _logger.LogError($"Movie with id: {id} can't be found. Reason: not exist");
                throw new NotFoundException($"Movie with id: {id} not found");
            }

            var result = _mapper.Map<MovieDto>(movie);

            return result;
        }

        public PagedResult<MovieDto> GetAllMovies(MovieQuery query)
        {
            var baseQuery = _dbContext
                .Movies
                .Include(m => m.Director)
                .Include(m => m.Country)
                .Include(m => m.Genre)
                .Include(m => m.MovieRatings)
                .Where(m => query.SearchPhrase == null || (m.Name.ToLower().Contains(query.SearchPhrase.ToLower()) || m.Description.ToLower().Contains(query.SearchPhrase.ToLower())));


            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Movie, object>>>
                {
                    {nameof(Movie.Name), x => x.Name },
                    {nameof(Movie.Genre), x => x.Genre },
                    {nameof(Movie.Year), x => x.Year },
                    {nameof(Movie.Country), x => x.Country },
                    {nameof(Movie.Director), x => x.Director }
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                baseQuery = query.SortOrder == SortOrder.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }
            

            var movies = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            

            var moviesDtos = _mapper.Map<List<MovieDto>>(movies);
    
            var result = new PagedResult<MovieDto>(moviesDtos, baseQuery.Count(), query.PageSize, query.PageNumber);

            return result;
        }

        public int Create(CreateMovieDto dto)
        {
            var movie = _mapper.Map<Movie>(dto);
            var genreName = movie.Genre.Name;
            var countryName = movie.Country.Name;
            var directorSurname = movie.Director.Surname;
            var directorName = movie.Director.Name;
            var existingGenre = _dbContext.Genres.FirstOrDefault(g => g.Name == genreName);
            var existingCountry = _dbContext.Countries.FirstOrDefault(g => g.Name == countryName);
            var existingDirector = _dbContext.Directors.FirstOrDefault(g => g.Name == directorName && g.Surname == directorSurname);

            var existingMovie = _dbContext.Movies.FirstOrDefault(g => g.Name == movie.Name);


            if (existingGenre != null)
            {
                movie.Genre = existingGenre;
            }

            if (existingCountry != null)
            {
                movie.Country = existingCountry;
            }

            if (existingDirector != null)
            {
                movie.Director = existingDirector;
            }

            if (existingMovie != null)
            {
                return -1;
            }

            _dbContext.Movies.Add(movie);

            _dbContext.SaveChanges();

            return movie.Id;
        }

        public void Update(UpdateMovieDto dto, int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                _logger.LogError($"Movie with id: {id} can't be updated. Reason: not exist");
                throw new NotFoundException($"Movie with id: {id} not found");
                
            }

            movie.Name = dto.Name;
            movie.Description = dto.Description;
            movie.Year = dto.Year;

            
            _dbContext.SaveChanges();
            
        }

        public bool Delete(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return false;
            }

            _dbContext.Remove(movie);
            _dbContext.SaveChanges();
            return true;
        }


    }
}

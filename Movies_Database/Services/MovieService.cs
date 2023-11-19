using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies_Database.Entities;
using Movies_Database.Models;

namespace Movies_Database.Services
{

    public interface IMovieService
    {
        MovieDto GetMovieById(int id);
        IEnumerable<MovieDto> GetAllMovies();
        int Create(CreateMovieDto dto);
        bool Update(UpdateMovieDto dto, int id);
    }
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _dbContext;
        private readonly IMapper _mapper;
        public MovieService(MovieDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
                return null;
            }

            var result = _mapper.Map<MovieDto>(movie);

            return result;
        }

        public IEnumerable<MovieDto> GetAllMovies()
        {
            var movies = _dbContext
                .Movies
                .Include(m => m.Director)
                .Include(m => m.Country)
                .Include(m => m.Genre)
                .Include(m => m.MovieRatings)
                .ToList();

            

            var moviesDtos = _mapper.Map<List<MovieDto>>(movies);

            return moviesDtos;
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

        public bool Update(UpdateMovieDto dto, int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return false;
            }

            movie.Name = dto.Name;
            movie.Description = dto.Description;
            movie.Year = dto.Year;

            
            _dbContext.SaveChanges();
            return true; ;
        }


    }
}

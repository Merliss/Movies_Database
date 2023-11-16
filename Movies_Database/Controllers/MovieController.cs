using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_Database.Entities;
using Movies_Database.Models;

namespace Movies_Database.Controllers
{

    [Route("api/movie")]
    public class MovieController : Controller
    {
        private readonly MovieDbContext _dbContext;
        private readonly IMapper _mapper;

        public MovieController(MovieDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> GetAll()
        {
            var movies = _dbContext
                .Movies
                .Include(m => m.Director)
                .Include(m => m.Country)
                .Include(m => m.Genre)
                .Include(m => m.MovieRatings)
                .ToList();
            
            var moviesDtos = _mapper.Map<List<MovieDto>>(movies);

            return Ok(moviesDtos);
        }


        [HttpGet("{id}")]
        public ActionResult<MovieDto> Get([FromRoute] int id)
        {
            var movie = _dbContext
                .Movies
                .Include(m => m.Director)
                .Include(m => m.Country)
                .Include(m => m.Genre)
                .Include(m => m.MovieRatings)
                .FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = _mapper.Map<MovieDto>(movie);
            return Ok(movieDto);
        }

        [HttpPost]
        public ActionResult CreateMovie([FromBody] CreateMovieDto dto)
        {
            var movie = _mapper.Map<Movie>(dto);
            var genreName = movie.Genre.Name;
            var countryName = movie.Country.Name;
            var directorSurname = movie.Director.Surname;
            var directorName = movie.Director.Name;
            var existingGenre = _dbContext.Genres.FirstOrDefault(g => g.Name == genreName);
            var existingCountry = _dbContext.Countries.FirstOrDefault(g => g.Name == countryName);
            var existingDirector = _dbContext.Directors.FirstOrDefault(g => g.Name == directorName && g.Surname == directorSurname);
            
            


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



            _dbContext.Movies.Add(movie);

            _dbContext.SaveChanges();

            return Created($"/api/movie/{movie.Id}", null);

        }

    }
}

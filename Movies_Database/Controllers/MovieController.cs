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
    }
}

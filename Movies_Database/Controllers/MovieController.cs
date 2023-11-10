using Microsoft.AspNetCore.Mvc;
using Movies_Database.Entities;
using Movies_Database.Models;

namespace Movies_Database.Controllers
{

    [Route("api/movie")]
    public class MovieController : Controller
    {
        private readonly MovieDbContext _dbContext;

        public MovieController(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> GetAll()
        {
            var movies = _dbContext
                .Movies
                .ToList();
            return Ok(movies);
        }


        [HttpGet("{id}")]
        public ActionResult<MovieDto> Get([FromRoute] int id)
        {
            var movie = _dbContext
                .Movies
                .FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }
    }
}

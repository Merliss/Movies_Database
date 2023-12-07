using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies_Database.Entities;
using Movies_Database.Models;
using Movies_Database.Services;

namespace Movies_Database.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    [Authorize]
    public class MovieRatingController : Controller
    {
        private readonly IMovieRatingService _movieRatingService;

        public MovieRatingController(IMovieRatingService movieRatingService)
        {
            _movieRatingService = movieRatingService;
        }

        [HttpGet("{id}")]

        public ActionResult<IEnumerable<MovieRatingDto>> GetAllByMovie([FromRoute] int id)
        {
            var ratings = _movieRatingService.GetAllByMovie(id);

            return Ok(ratings);
        }

        [HttpPost]
        
        public ActionResult CreateMovieRating([FromBody] CreateMovieRatingDto dto)
        {
            //var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _movieRatingService.Create(dto);

            if (id == -1)
            {
                return BadRequest(dto.MovieName);
            }



            return Created($"/api/movie/{id}", null);

        }


        [HttpPut]
        public ActionResult UpdateMovieRating([FromBody] CreateMovieRatingDto dto)
        {
            var id = _movieRatingService.Update(dto);

            if(id == -1)
            {
                return BadRequest(dto.MovieName);
            }
            return Created($"/api/movie/{id}", null);


        }
    }
}

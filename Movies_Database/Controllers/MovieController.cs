using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_Database.Entities;
using Movies_Database.Models;
using Movies_Database.Services;

namespace Movies_Database.Controllers
{

    [Route("api/movie")]
    [ApiController]
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieDto>> GetAll([FromQuery]string searchPhrase)
        {
            var moviesDtos = _movieService.GetAllMovies(searchPhrase);

            return Ok(moviesDtos);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<MovieDto> Get([FromRoute] int id)
        {
            var movieDto = _movieService.GetMovieById(id);


            
            return Ok(movieDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult CreateMovie([FromBody] CreateMovieDto dto)
        {
            
            var id = _movieService.Create(dto);
           
            if (id == -1)
            {
                return BadRequest(dto.Name);
            }


            
            return Created($"/api/movie/{id}", null);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult UpdateMovie([FromBody] UpdateMovieDto dto, [FromRoute] int id)

        {
            
            _movieService.Update(dto, id);

            return Ok();

        }

    }
}

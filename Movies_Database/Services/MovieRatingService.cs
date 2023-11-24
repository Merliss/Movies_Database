using System.Xml;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies_Database.Entities;
using Movies_Database.Models;

namespace Movies_Database.Services
{

    public interface IMovieRatingService
    {
        IEnumerable<MovieRatingDto> GetAllByMovie(int movieId);
        int Create(CreateMovieRatingDto ratingDto, int UserId);
    }
    public class MovieRatingService : IMovieRatingService
    {
        private readonly MovieDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<MovieRatingService> _logger;

        public MovieRatingService(MovieDbContext dbContext, IMapper mapper, ILogger<MovieRatingService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<MovieRatingDto> GetAllByMovie(int movieId)
        {
            var ratings = _dbContext
                .MovieRatings
                .Include(m => m.Movie)
                .Include(m => m.Users)
                .Where(m => m.MovieId == movieId)
                .ToList();

            var ratingsDto = _mapper.Map<List<MovieRatingDto>>(ratings);

            return ratingsDto;

        }

        public int Create(CreateMovieRatingDto dto, int UserIdCreating)
        {
            var rating = _mapper.Map<MovieRating>(dto);
            rating.UsersId = UserIdCreating;
            rating.MovieId = _dbContext.Movies.FirstOrDefault(m => m.Name == dto.MovieName).Id;
            var UserId = rating.UsersId;
            var MovieRatingId = rating.MovieId;

            var existingRating = _dbContext.MovieRatings.FirstOrDefault(r => r.UsersId == UserId && r.MovieId == MovieRatingId);
            


            

            if (existingRating != null)
            {
                return -1;
            }

            _dbContext.MovieRatings.Add(rating);

            _dbContext.SaveChanges();

            return rating.Id;
        }


    }
}

using System.Xml;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies_Database.Entities;
using Movies_Database.Exceptions;
using Movies_Database.Models;

namespace Movies_Database.Services
{

    public interface IMovieRatingService
    {
        IEnumerable<MovieRatingDto> GetAllByMovie(int movieId);
        int Create(CreateMovieRatingDto ratingDto);

        int Update(CreateMovieRatingDto dto);
    }
    public class MovieRatingService : IMovieRatingService
    {
        private readonly MovieDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<MovieRatingService> _logger;
        private readonly IUserContextService _userContextService;

        public MovieRatingService(MovieDbContext dbContext, IMapper mapper, ILogger<MovieRatingService> logger, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
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

        public int Create(CreateMovieRatingDto dto)
        {
            var rating = _mapper.Map<MovieRating>(dto);
            rating.UsersId = (int)_userContextService.GetUserId;
            rating.MovieId = _dbContext.Movies.FirstOrDefault(m => m.Name == dto.MovieName).Id;
            var UserId = rating.UsersId;
            var MovieIdFromRating = rating.MovieId;

            var existingRating = _dbContext.MovieRatings.FirstOrDefault(r => r.UsersId == UserId && r.MovieId == MovieIdFromRating);
            


            

            if (existingRating != null)
            {
                return -1;
            }

            _dbContext.MovieRatings.Add(rating);

            _dbContext.SaveChanges();

            return rating.Id;
        }

        public int Update(CreateMovieRatingDto dto)
        {
            var rating = _mapper.Map<MovieRating>(dto);
            rating.UsersId = (int)_userContextService.GetUserId;
            rating.MovieId = _dbContext.Movies.FirstOrDefault(m => m.Name == dto.MovieName).Id;
            var UserId = rating.UsersId;
            var MovieIdFromRating = rating.MovieId;

            var existingRating = _dbContext.MovieRatings.FirstOrDefault(r => r.UsersId == UserId && r.MovieId == MovieIdFromRating);





            if (existingRating == null)
            {
                _logger.LogError($"Rating with movie name: {dto.MovieName} can't be updated. Reason: not exist");
                throw new NotFoundException($"Movie with name: {dto.MovieName} not found");

            }
            existingRating.Rating = dto.Rating;
            existingRating.IsFavorite = dto.IsFavorite;


            _dbContext.SaveChanges();

            return rating.Id;
        }

        
    }
}

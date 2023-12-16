using System.Security.Claims;
using System.Xml;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies_Database.Authorization;
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
        bool Delete(int id);
    }
    public class MovieRatingService : IMovieRatingService
    {
        private readonly MovieDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<MovieRatingService> _logger;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public MovieRatingService(MovieDbContext dbContext, IMapper mapper, ILogger<MovieRatingService> logger, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
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

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, existingRating, new ResourceOperationsRequirement(ResourceOperation.Update)).Result;
           
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }


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


        public bool Delete(int id)
        {
            var rating = _dbContext.MovieRatings.FirstOrDefault(mr => mr.Id == id);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, rating, new ResourceOperationsRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            if (rating == null)
            {
                _logger.LogError($"Rating with movie name: {id} can't be updated. Reason: not exist");
                throw new NotFoundException($"Rating with: {id} not found");
            }

            if ( rating != null && authorizationResult.Succeeded) 
            {
                _dbContext.Remove(rating);
                _dbContext.SaveChanges();

                return true;
            }
            
            return false;

        }
        
    }
}

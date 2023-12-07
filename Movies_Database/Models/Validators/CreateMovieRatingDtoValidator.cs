using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Movies_Database.Entities;

namespace Movies_Database.Models.Validators
{



    public class CreateMovieRatingDtoValidator : AbstractValidator<CreateMovieRatingDto>
    {

        public CreateMovieRatingDtoValidator(MovieDbContext dbContext)
        {


            RuleFor(x => x.Rating)
                .NotEmpty()
                .InclusiveBetween(0, 10)
                .WithMessage("Rating must be between 0 to 10");

            RuleFor(x => x.MovieName)
                .NotEmpty();
        }



    }
}

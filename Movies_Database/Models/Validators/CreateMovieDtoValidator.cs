using FluentValidation;
using Movies_Database.Entities;

namespace Movies_Database.Models.Validators
{
    public class CreateMovieDtoValidator : AbstractValidator<CreateMovieDto>
    {

        public CreateMovieDtoValidator(MovieDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty();


            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.DirectorName)
                .NotEmpty();

            RuleFor(x => x.DirectorSurname)
                .NotEmpty();

            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(x => x.Genre)
                .NotEmpty()
                .MaximumLength(35);

        }
    }
}

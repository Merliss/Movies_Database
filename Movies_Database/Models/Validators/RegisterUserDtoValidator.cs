using FluentValidation;
using Movies_Database.Entities;

namespace Movies_Database.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto> //FluentValidation
    {
        public RegisterUserDtoValidator(MovieDbContext dbContext)
        {
            RuleFor(x => x.Username).NotEmpty();


            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .Equal(p => p.Password);
                


            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailExist = dbContext.Users.Any(x => x.Email == value);
                    if (emailExist)
                    {
                        context.AddFailure("Email already exist"); //dodać takie sprawdzenia do reszty encji
                    }
                });

            RuleFor(x => x.Username)
                .Custom((value, context) =>
                {
                    var usernameExist = dbContext.Users.Any(x => x.Username == value);
                    if (usernameExist)
                    {
                        context.AddFailure("Username already exist");
                    }
                });
        }

    }
}

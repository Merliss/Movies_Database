using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace Movies_Database.Models.Validators
{
    public class MovieQueryValidator : AbstractValidator<MovieQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15, 20 };
        public MovieQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be: [{string.Join(",",allowedPageSizes)}]");
                }
            });
        }

    }
}

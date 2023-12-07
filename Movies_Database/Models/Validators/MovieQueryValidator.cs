using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Movies_Database.Entities;

namespace Movies_Database.Models.Validators
{
    public class MovieQueryValidator : AbstractValidator<MovieQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15, 20 };
        private string[] allowedSortByColumnNames = { nameof(Movie.Name), nameof(Movie.Genre), nameof(Movie.Year), nameof(Movie.Country), nameof(Movie.Director) };


        public MovieQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be: [{string.Join(",", allowedPageSizes)}]");
                }
            });

            RuleFor(r => r.SortBy).Must(value => string.IsNullOrEmpty(value)|| allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort is optional or must be in [{string.Join(",",allowedSortByColumnNames)}]");
        }

    }
}

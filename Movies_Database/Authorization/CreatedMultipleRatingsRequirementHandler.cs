using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Movies_Database.Entities;

namespace Movies_Database.Authorization
{
    public class CreatedMultipleRatingsRequirementHandler : AuthorizationHandler<CreatedMultipleRatingsRequirement>
    {
        private readonly MovieDbContext _dbContext;

        public CreatedMultipleRatingsRequirementHandler(MovieDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRatingsRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);


            var createdRatingsCount = _dbContext
                .MovieRatings
                .Count(r => r.UsersId == userId);
        
            if (createdRatingsCount >= requirement.MinimumRatingsCreated ) 
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

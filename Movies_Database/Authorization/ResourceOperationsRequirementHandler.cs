using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Movies_Database.Entities;

namespace Movies_Database.Authorization
{
    public class ResourceOperationsRequirementHandler : AuthorizationHandler<ResourceOperationsRequirement,MovieRating>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationsRequirement requirement, MovieRating movieRating)
        {
           if (requirement.ResourceOperation == ResourceOperation.Read ||
                requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

           var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (movieRating.UserId == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}

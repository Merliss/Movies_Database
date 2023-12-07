using Microsoft.AspNetCore.Authorization;

namespace Movies_Database.Authorization
{
    public class CreatedMultipleRatingsRequirement : IAuthorizationRequirement
    {

        public int MinimumRatingsCreated { get;}

        public CreatedMultipleRatingsRequirement(int minimumRatingsCreated)
        {

            MinimumRatingsCreated = minimumRatingsCreated;

        }



    }
}

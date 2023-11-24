using Microsoft.AspNetCore.Authorization;

namespace Movies_Database.Authorization
{


    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class ResourceOperationsRequirement : IAuthorizationRequirement
    {
        public ResourceOperationsRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }

        public ResourceOperation ResourceOperation { get; set; }

    }
}

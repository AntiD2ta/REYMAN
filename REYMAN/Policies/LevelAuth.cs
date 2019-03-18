using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAYMAN.Policies
{
    public class LevelAuthRequirement : IAuthorizationRequirement
    {
        public Dictionary<string, int> Permissions { get; }
        public int Priority { get; }
        public string TypeClaimPermission { get; }

        public LevelAuthRequirement(Dictionary<string, int> permissions, string typeClaimPermission, int priority)
        {
            Permissions = permissions;
            TypeClaimPermission = typeClaimPermission;
            Priority = priority;
        }

    }

    public class LevelHandler : AuthorizationHandler<LevelAuthRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            LevelAuthRequirement requirement)
        {
            var permissions = context.User.Claims.ToList();

            foreach (var permission in permissions)
            {
                if (permission.Type == requirement.TypeClaimPermission &&
                   requirement.Permissions[permission.Value] >= requirement.Priority)
                {
                    context.Succeed(requirement);
                    break;
                }
            }

            return Task.CompletedTask;
        }
    }
}

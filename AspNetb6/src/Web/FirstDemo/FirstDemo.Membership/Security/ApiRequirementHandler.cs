using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Membership.Security
{
    public class ApiRequirementHandler :
          AuthorizationHandler<ApiRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               ApiRequirement requirement)
        {
            var claim = context.User.FindFirst("ViewTestPage");
            if (claim != null && bool.Parse(claim.Value))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}

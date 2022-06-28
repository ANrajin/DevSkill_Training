using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Membership.Securities
{
    public class TestRequirementHandler : AuthorizationHandler<TestRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            TestRequirement requirement)
        {
            if(context.User.HasClaim(x => x.Type == "ViewTestPage" && x.Value == "true"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}

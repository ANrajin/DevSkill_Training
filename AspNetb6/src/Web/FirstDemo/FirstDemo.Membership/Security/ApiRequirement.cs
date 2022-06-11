using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstDemo.Membership.Security
{
    public class ApiRequirement : IAuthorizationRequirement
    {
        public ApiRequirement()
        {
        }
    }
}

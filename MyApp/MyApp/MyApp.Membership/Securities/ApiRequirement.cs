using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Membership.Securities
{
    public class ApiRequirement:IAuthorizationRequirement
    {
        public ApiRequirement()
        {

        }
    }
}

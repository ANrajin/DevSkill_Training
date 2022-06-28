using MyApp.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Membership.Seeds
{
    internal static class RolesSeed
    {
        internal static Role[] Roles
        {
            get
            {
                return new Role[]
                {
                    new Role{ 
                        Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN", 
                        ConcurrencyStamp =  DateTime.Now.Ticks.ToString()  
                    },
                    new Role{ 
                        Id = Guid.NewGuid(), Name = "Customer", NormalizedName = "CUSTOMER", 
                        ConcurrencyStamp =  DateTime.Now.AddMinutes(1).Ticks.ToString()  
                    }
                };
            }
        }
    }
}

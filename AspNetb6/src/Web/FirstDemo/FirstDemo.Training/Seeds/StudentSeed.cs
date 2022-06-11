using FirstDemo.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Training.Seeds
{
    internal class StudentSeed
    {
        internal Student[] Students
        {
            get
            {
                return new Student[]
                {
                    new Student{ Id = -1, Name = "Asif", CGPA = 3, Address = "Dhaka" },
                    new Student{ Id = -2, Name = "Rashed", CGPA = 3.9, Address = "Khulna" },
                    new Student{ Id = -3, Name = "Monir", CGPA = 2.9, Address = "Rajshahi" }
                };
            }
        }
    }
}

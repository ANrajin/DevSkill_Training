using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionAnotherExample
{
    public interface IVehicle
    {
        double CurrentSpeed { get; set; }

        public void SpeedUp(double speed)
        {
            CurrentSpeed += 10* speed;
        }
    }
}

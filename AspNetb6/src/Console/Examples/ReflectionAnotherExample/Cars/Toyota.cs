using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionAnotherExample.Cars
{
    public class Toyota : ICar, IVehicle
    {
        public string Model { get; set; }
        public double Speed { get; set; }
        public string Color { get; set; }
        public double CurrentSpeed { get; set; }

        void ICar.SpeedUp(double speed)
        {
            CurrentSpeed += 100* speed;
        }

        void IVehicle.SpeedUp(double speed)
        {
            CurrentSpeed += 1000 * speed;
        }

        public void SpeedUp(double speed)
        {
            
        }

        public Toyota()
        {
            Model = "Toyota";
        }
    }
}

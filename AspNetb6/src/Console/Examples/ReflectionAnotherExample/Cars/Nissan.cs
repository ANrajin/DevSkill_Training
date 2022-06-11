using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionAnotherExample.Cars
{
    public class Nissan : ICar
    {
        public string Model { get; set; }
        public double Speed { get; set; }
        public string Color { get; set; }
        public double CurrentSpeed { get; set; }

        public Nissan()
        {
            Model = "Nissan";
        }
    }
}

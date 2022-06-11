using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionAnotherExample
{
    public interface ICar
    {
        string Model { get; set; }
        double Speed { get; set; }
        string Color { get; set; }
        double CurrentSpeed { get; set; }

        public void Start()
        {
            CurrentSpeed = 0;
        }

        public void SpeedUp(double speed)
        {
            CurrentSpeed += speed;
        }

        public void SpeedDown(double speed)
        {
            CurrentSpeed -= speed;
        }
    }
}

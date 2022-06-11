using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPatterns
{
    public class CarBuilder
    {
        private Car _car;

        public CarBuilder()
        {
            _car = new Car();
        }

        public CarBuilder AddEngine()
        {
            _car.Engine = new Engine();
            return this;
        }
        public CarBuilder AddColor(string color)
        {
            _car.Color = color;
            return this;
        }
        public CarBuilder AddDoor()
        {
            return this;
        }

        public CarBuilder AddWheels()
        {
            return this;
        }

        public Car Build()
        {
            return _car;
        }
    }
}

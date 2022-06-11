using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPatterns
{
    public class CarFactory
    {
        public static Car CreateCar(Presets preset)
        {
            if (preset == Presets.Preset1)
            {
                return new Toyota() { Color = "red", Wheels = "Large" };
            }
            else if (preset == Presets.Preset2)
            {
                return new BMW() { Color = "blue", Wheels = "Medium" };
            }
            else
                return null;
        }
    }
}

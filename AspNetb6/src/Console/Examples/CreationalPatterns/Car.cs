using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPatterns
{
    public class Car
    {
        public string Color { get; set; }
        public string Wheels { get; set; }
        public object Engine { get; set; }
    }

    public enum Presets
    {
        Preset1,
        Preset2,
        Preset3
    }

    public class Engine
    {

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPatterns
{
    public class AmericanMilitaryFactory : MilitaryFactory
    {
        public override Fighter GetFighter()
        {
            return new F16();
        }

        public override Ship GetShip()
        {
            return new Carrier();
        }
    }
}

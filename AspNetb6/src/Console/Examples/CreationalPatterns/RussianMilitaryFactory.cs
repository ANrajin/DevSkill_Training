using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationalPatterns
{
    public class RussianMilitaryFactory : MilitaryFactory
    {
        public override Fighter GetFighter()
        {
            return new Mig29();
        }

        public override Ship GetShip()
        {
            return new Submarine();
        }
    }
}

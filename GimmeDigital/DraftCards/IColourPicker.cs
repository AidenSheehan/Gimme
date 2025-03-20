using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public enum Colour {Red, Green, Blue, Undefined}

    public interface IColourPicker : ICloneable
    {
        public Colour Colour {get;}

        public IEnumerable<Colour> PotentialColours {get;}
    }
}
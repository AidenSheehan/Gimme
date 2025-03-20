using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public class StandardColourPicker: IColourPicker
    {
        public Colour Colour { get; init; }

        public IEnumerable<Colour> PotentialColours => [Colour];

        public StandardColourPicker(Colour colour)
        {
            if (colour == Colour.Undefined)
                throw new ArgumentException("Standard colours cannot be undefined");
            Colour = colour;
        }

        public object Clone() => new StandardColourPicker(Colour);

        public override string ToString() => Colour.ToString();
    }
}

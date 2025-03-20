using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public class RainbowColourPicker : IColourPicker
    {
        public Colour Colour { get; private set; }

        public IEnumerable<Colour> PotentialColours => [Colour.Red, Colour.Green, Colour.Blue];

        public RainbowColourPicker() => Colour = Colour.Undefined;

        protected RainbowColourPicker(RainbowColourPicker other) => Colour = other.Colour;

        public object Clone() => new RainbowColourPicker(this);

        public override string ToString() => Colour == Colour.Undefined ? "Rainbow" : $"{Colour}?";

        public void ChooseColour(Colour colour) => Colour = colour;
    }
}

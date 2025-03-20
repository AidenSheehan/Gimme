using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public class DraftCard(IColourPicker colourPicker, ISymbolPicker symbolPicker) : Card, ICloneable
    {
        public IColourPicker ColourPicker { get; init; } = colourPicker;
        public ISymbolPicker SymbolPicker { get; init; } = symbolPicker;

        public object Clone()
        {
            return new DraftCard(
                (IColourPicker)ColourPicker.Clone(),
                (ISymbolPicker)SymbolPicker.Clone()
            );
        }

        public override string ToString()
        {
            return $"{ColourPicker} {SymbolPicker}";
        }

    }
}

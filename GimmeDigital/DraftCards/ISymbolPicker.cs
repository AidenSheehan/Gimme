using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public enum Symbol {Star, One, Two, Three, Four, Five, Six, Seven, Undefined}
    public interface ISymbolPicker : ICloneable
    {
        public Symbol Symbol {get;}

        public IEnumerable<Symbol> PotentialSymbols {get;}
    }
}
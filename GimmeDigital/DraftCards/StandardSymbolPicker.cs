using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public class StandardSymbolPicker : ISymbolPicker
    {
        public Symbol Symbol { get; init; }

        public IEnumerable<Symbol> PotentialSymbols => [Symbol];

        public object Clone() => new StandardSymbolPicker(Symbol);

        public StandardSymbolPicker(Symbol symbol)
        {
            if (symbol == Symbol.Undefined)
                throw new ArgumentException("Standard symbols cannot be undefined");
            Symbol = symbol;
        }

        public override string ToString() => Symbol.ToString();
    }
}

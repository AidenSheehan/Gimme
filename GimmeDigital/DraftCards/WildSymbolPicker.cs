using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public class WildSymbolPicker : ISymbolPicker
    {
        public Symbol Symbol {get; private set;}

        public IEnumerable<Symbol> PotentialSymbols =>
            [
                Symbol.One,
                Symbol.Two,
                Symbol.Three,
                Symbol.Four,
                Symbol.Five,
                Symbol.Six,
                Symbol.Seven
            ];

        public object Clone() => new WildSymbolPicker(this);

        public WildSymbolPicker()
        {
            Symbol = Symbol.Undefined;
        }

        protected WildSymbolPicker(WildSymbolPicker other)
        {
            Symbol = other.Symbol;
        }

        public void ChooseSymbol(Symbol symbol)
        {
            if (symbol == Symbol.Star)
                throw new ArgumentException("Wildcards cannot be stars");
            Symbol = symbol;
        }

        public override string ToString() => Symbol == Symbol.Undefined ? "?" : $"{Symbol}?";
    }
}

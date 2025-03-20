using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public interface IDeckFactory<T>
        where T : Card
    {
        public static abstract Deck<T> CreateDeck();
    }
}

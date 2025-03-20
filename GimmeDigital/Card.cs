using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public abstract class Card
    {
        public bool IsRevealed {get; private set;} = false; // Whether ALL players can see the card face-up

        public void Reveal() => IsRevealed = true;
        public void Hide() => IsRevealed = false;
    }
}
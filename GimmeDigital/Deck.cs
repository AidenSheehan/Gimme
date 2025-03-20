using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public class Deck<T>
        where T : Card
    {
        private readonly List<T> cards;
        private readonly Random rng;
        public int Count => cards.Count;

        public Deck(IEnumerable<T> startingCards)
        {
            cards = [];
            rng = new Random();
            AddCards(startingCards);
            HideAll();
            Shuffle();
        }

        public void AddCards(IEnumerable<T> cardsToAdd) => cards.AddRange(cardsToAdd);

        public void Shuffle() => cards.Shuffle(rng);

        public IEnumerable<T> DrawCards(int amountToDraw, bool andReveal = false)
        {
            var drawnCards = cards.Take(amountToDraw);
            if (andReveal)
            {
                foreach (Card c in drawnCards)
                {
                    c.Reveal();
                }
            }
            return drawnCards;
        }

        public void HideAll() => cards.ForEach(c => c.Hide());
    }
}

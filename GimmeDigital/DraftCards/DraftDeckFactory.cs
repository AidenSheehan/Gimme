using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public abstract class DraftDeckFactory : IDeckFactory<DraftCard>
    {
        private const int SIMPLE_NUMBER_AMOUNT = 3;
        private const int RAINBOW_NUMBER_AMOUNT = 2;
        private const int SIMPLE_STAR_AMOUNT = 3;
        private const int RAINBOW_STAR_AMOUNT = 2;
        private const int SIMPLE_WILD_AMOUNT = 2;
        private const int RAINBOW_WILD_AMOUNT = 2;

        public static Deck<DraftCard> CreateDeck()
        {
            List<DraftCard> cardsToAdd = [];
            AddNumberCards(cardsToAdd);
            AddStarCards(cardsToAdd);
            AddWildCards(cardsToAdd);
            return new Deck<DraftCard>(cardsToAdd);
        }

        private static void AddNumberCards(List<DraftCard> cardsToAdd)
        {
            for (int i = 1; i <= 7; i++)
            {
                foreach (Colour colour in new List<Colour>() { Colour.Red, Colour.Green, Colour.Blue })
                {
                    DraftCard simpleNumber =
                        new(
                            new StandardColourPicker(colour),
                            new StandardSymbolPicker(i.AsSymbol())
                        );
                    cardsToAdd.AddRange(
                        simpleNumber.CreateCopies(SIMPLE_NUMBER_AMOUNT).Cast<DraftCard>()
                    );
                }
                DraftCard rainbowNumber =
                    new(new RainbowColourPicker(), new StandardSymbolPicker(i.AsSymbol()));
                cardsToAdd.AddRange(
                    rainbowNumber.CreateCopies(RAINBOW_NUMBER_AMOUNT).Cast<DraftCard>()
                );
            }
        }

        private static void AddStarCards(List<DraftCard> cardsToAdd)
        {
            foreach (Colour colour in new List<Colour>() { Colour.Red, Colour.Green, Colour.Blue })
            {
                DraftCard simpleStar =
                    new(new StandardColourPicker(colour), new StandardSymbolPicker(Symbol.Star));
                cardsToAdd.AddRange(simpleStar.CreateCopies(SIMPLE_STAR_AMOUNT).Cast<DraftCard>());
            }
            DraftCard rainbowStar =
                new(new RainbowColourPicker(), new StandardSymbolPicker(Symbol.Star));
            cardsToAdd.AddRange(rainbowStar.CreateCopies(RAINBOW_STAR_AMOUNT).Cast<DraftCard>());
        }

        private static void AddWildCards(List<DraftCard> cardsToAdd)
        {
            foreach (Colour colour in new List<Colour>() { Colour.Red, Colour.Green, Colour.Blue })
            {
                DraftCard simpleWild =
                    new(new StandardColourPicker(colour), new WildSymbolPicker());
                cardsToAdd.AddRange(simpleWild.CreateCopies(SIMPLE_WILD_AMOUNT).Cast<DraftCard>());
            }
            DraftCard rainbowWild = new(new RainbowColourPicker(), new WildSymbolPicker());
            cardsToAdd.AddRange(rainbowWild.CreateCopies(RAINBOW_WILD_AMOUNT).Cast<DraftCard>());
        }
    }
}

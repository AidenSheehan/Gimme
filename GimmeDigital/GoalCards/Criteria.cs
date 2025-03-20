using System.Diagnostics;

namespace GimmeDigital.GoalCards
{
    public static class Criteria
    {
        public static Criterion Blank() =>
            pool =>
            {
                Debug.WriteLine("Blank Criterion");
                return true;
            };

        public static Criterion AmountSymbol(int amount, Symbol symbol) =>
            pool => pool.WhereIsSymbol(symbol).Count() >= amount;

        public static Criterion AmountColour(int amount, Colour colour) =>
            pool => pool.WhereIsColour(colour).Count() >= amount;

        public static Criterion EightPair(Colour colour)
        {
            return pool =>
            {
                var cards = pool.WhereIsAnyNumber().WhereIsColour(colour);
                return cards.Any(card =>
                    cards.Any(other =>
                        other != card
                        && other.SymbolPicker.Symbol.AsNumber()
                            + card.SymbolPicker.Symbol.AsNumber()
                            == 8
                    )
                );
            };
        }

        public static Criterion AmountEven(int amount) =>
            pool => pool.WhereIsEven().Count() >= amount;

        public static Criterion AmountOdd(int amount) =>
            pool => pool.WhereIsOdd().Count() >= amount;

        public static Criterion AmountRange(int amount, IEnumerable<int> range) =>
            pool => range.Sum(v => pool.WhereIsSymbol(v.AsSymbol()).Count()) >= amount;

        public static Criterion SumColour(int threshold, Colour colour)
        {
            return pool =>
                pool.WhereIsColour(colour)
                    .WhereIsAnyNumber()
                    .Sum(c => c.SymbolPicker.Symbol.AsNumber()) >= threshold;
        }

        public static Criterion MaxSum(int threshold)
        {
            return pool =>
                pool.WhereIsAnyNumber().Sum(c => c.SymbolPicker.Symbol.AsNumber()) <= threshold;
        }

        public static Criterion SequenceLength(int minLength)
        {
            return pool =>
            {
                int longestSequenceLength = 0;
                int[] arr = pool.WhereIsAnyNumber()
                    .DistinctBy(c => c.SymbolPicker.Symbol)
                    .Select(c => c.SymbolPicker.Symbol.AsNumber())
                    .ToArray();
                HashSet<int> S = [.. arr];
                for (int i = 0; i < S.Count; i++)
                {
                    if (S.Contains(arr[i] - 1))
                        continue;
                    int j = arr[i];
                    while (S.Contains(j))
                        j++;

                    longestSequenceLength = Math.Max(longestSequenceLength, j - arr[i]);
                }
                return longestSequenceLength >= minLength;
            };
        }

        public static Criterion SequenceLengthSameColour(int minLength)
        {
            return pool =>
            {
                IEnumerable<IEnumerable<DraftCard>> pools =
                [
                    pool.WhereIsColour(Colour.Red),
                    pool.WhereIsColour(Colour.Green),
                    pool.WhereIsColour(Colour.Blue)
                ];
                return pools.Any(p => SequenceLength(minLength)(p));
            };
        }

        public static Criterion ContainsRange(IEnumerable<int> range) =>
            pool => range.All(r => pool.WhereIsSymbol(r.AsSymbol()).Any());

        private static IEnumerable<DraftCard> WhereIsColour(
            this IEnumerable<DraftCard> cards,
            Colour colour
        ) => cards.Where(c => c.ColourPicker.Colour == colour);

        private static IEnumerable<DraftCard> WhereIsSymbol(
            this IEnumerable<DraftCard> cards,
            Symbol symbol
        ) => cards.Where(c => c.SymbolPicker.Symbol == symbol);

        private static IEnumerable<DraftCard> WhereIsAnyNumber(this IEnumerable<DraftCard> cards) =>
            cards.Where(c => c.SymbolPicker.Symbol.IsNumber());

        private static IEnumerable<DraftCard> WhereIsOdd(this IEnumerable<DraftCard> cards) =>
            cards.WhereIsAnyNumber().Where(c => c.SymbolPicker.Symbol.AsNumber() % 2 == 1);

        private static IEnumerable<DraftCard> WhereIsEven(this IEnumerable<DraftCard> cards) =>
            cards.WhereIsAnyNumber().Where(c => c.SymbolPicker.Symbol.AsNumber() % 2 == 0);
    }
}

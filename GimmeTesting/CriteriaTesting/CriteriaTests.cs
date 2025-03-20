using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GimmeDigital;
using GimmeDigital.GoalCards;
using Xunit;

namespace GimmeTesting.CriteriaTesting
{
    public class CriteriaTests
    {
        private static DraftCard CreateStandardCard(Colour colour, Symbol symbol) =>
            new(new StandardColourPicker(colour), new StandardSymbolPicker(symbol));

        private static IEnumerable<DraftCard> ToPool(DraftCard draftCard, uint copies) =>
            draftCard.CreateCopies(copies).Cast<DraftCard>();

        [Fact]
        public void AmountSymbol_Test()
        {
            var c = Criteria.AmountSymbol;
            var greenOne = CreateStandardCard(Colour.Green, Symbol.One);
            var redOne = CreateStandardCard(Colour.Red, Symbol.One);
            var greenFive = CreateStandardCard(Colour.Green, Symbol.Five);

            Assert.True(c(3, Symbol.One)(ToPool(greenOne, 3)));
            Assert.False(c(3, Symbol.One)(ToPool(greenOne, 2)));
            Assert.False(c(5, Symbol.One)(ToPool(greenOne, 3)));

            Assert.True(c(3, Symbol.One)(ToPool(greenOne, 2).Union(ToPool(redOne, 1))));

            Assert.False(c(3, Symbol.One)(ToPool(greenFive, 3)));
            Assert.False(c(3, Symbol.One)([]));
        }

        [Fact]
        public void AmountColour_Test()
        {
            var c = Criteria.AmountColour;
            var greenOne = CreateStandardCard(Colour.Green, Symbol.One);
            var redOne = CreateStandardCard(Colour.Red, Symbol.One);
            var greenFive = CreateStandardCard(Colour.Green, Symbol.Five);

            Assert.True(c(3, Colour.Green)(ToPool(greenOne, 3)));
            Assert.False(c(3, Colour.Green)(ToPool(greenOne, 2)));
            Assert.False(c(5, Colour.Green)(ToPool(greenOne, 3)));

            Assert.True(c(3, Colour.Green)(ToPool(greenOne, 2).Union(ToPool(greenFive, 1))));

            Assert.False(c(3, Colour.Green)(ToPool(redOne, 3)));
            Assert.False(c(3, Colour.Green)([]));
        }

        [Fact]
        public void EightPair_Test()
        {
            var c = Criteria.EightPair;
            var greenOne = CreateStandardCard(Colour.Green, Symbol.One);
            var redOne = CreateStandardCard(Colour.Red, Symbol.One);
            var greenFour = CreateStandardCard(Colour.Green, Symbol.Four);
            var greenSeven = CreateStandardCard(Colour.Green, Symbol.Seven);

            Assert.True(c(Colour.Green)(ToPool(greenOne, 1).Union(ToPool(greenSeven, 1))));
            Assert.False(c(Colour.Green)(ToPool(redOne, 1).Union(ToPool(greenSeven, 1))));
            Assert.False(c(Colour.Green)(ToPool(greenOne, 1).Union(ToPool(greenFour, 1))));
            Assert.True(c(Colour.Green)(ToPool(greenFour, 2)));
            Assert.False(c(Colour.Green)(ToPool(greenFour, 1)));
            Assert.False(c(Colour.Green)([]));
        }

        [Fact]
        public void AmountEven_Test()
        {
            var c = Criteria.AmountEven;
            var greenTwo = CreateStandardCard(Colour.Green, Symbol.Two);
            var greenFour = CreateStandardCard(Colour.Green, Symbol.Four);
            var greenSeven = CreateStandardCard(Colour.Green, Symbol.Seven);

            Assert.True(c(3)(ToPool(greenFour, 3)));
            Assert.False(c(5)(ToPool(greenFour, 3)));
            Assert.True(c(3)(ToPool(greenFour, 2).Union(ToPool(greenTwo, 1))));
            Assert.False(c(5)([]));
        }

        [Fact]
        public void AmountRange_Test()
        {
            var c = Criteria.AmountRange;
            var greenTwo = CreateStandardCard(Colour.Green, Symbol.Two);
            var greenThree = CreateStandardCard(Colour.Green, Symbol.Three);
            var greenOne = CreateStandardCard(Colour.Green, Symbol.One);

            Assert.True(c(3, [1, 2, 3])(ToPool(greenTwo, 3)));
            Assert.True(c(3, [1, 2, 3])(ToPool(greenTwo, 1).Union(ToPool(greenOne, 2))));
            Assert.False(c(3, [1, 2, 3])(ToPool(greenTwo, 1).Union(ToPool(greenOne, 1))));
            Assert.True(c(3, [1, 2, 3])([greenOne, greenTwo, greenThree]));
            Assert.False(c(3, [1, 2, 3])([]));
        }

        [Fact]
        public void SumColour_Test()
        {
            var c = Criteria.SumColour;
            var greenSeven = CreateStandardCard(Colour.Green, Symbol.Seven);
            var redSeven = CreateStandardCard(Colour.Red, Symbol.Seven);

            Assert.True(c(15, Colour.Green)(ToPool(greenSeven, 3)));
            Assert.False(c(15, Colour.Green)(ToPool(greenSeven, 2)));
            Assert.False(c(15, Colour.Green)(ToPool(redSeven, 3)));
            Assert.False(c(15, Colour.Green)([]));
        }

        [Fact]
        public void MaxSum_Test()
        {
            var c = Criteria.MaxSum;
            var greenSeven = CreateStandardCard(Colour.Green, Symbol.Seven);

            Assert.True(c(15)(ToPool(greenSeven, 2)));
            Assert.False(c(15)(ToPool(greenSeven, 3)));
            Assert.True(c(15)([]));
        }

        [Fact]
        public void SequenceLength_Test()
        {
            var c = Criteria.SequenceLength;
            var blueThree = CreateStandardCard(Colour.Blue, Symbol.Three);
            var greenFour = CreateStandardCard(Colour.Green, Symbol.Four);
            var blueFive = CreateStandardCard(Colour.Blue, Symbol.Five);
            var redSix = CreateStandardCard(Colour.Red, Symbol.Six);
            var greenSeven = CreateStandardCard(Colour.Green, Symbol.Seven);

            Assert.True(c(4)([greenFour, blueFive, redSix, greenSeven]));
            Assert.False(c(4)([greenFour, blueFive, redSix]));
            Assert.False(c(3)([blueThree, greenFour, redSix]));
            Assert.False(c(3)(ToPool(greenFour, 2).Union(ToPool(blueFive, 1))));
        }

        [Fact]
        public void SequenceLengthSameColour_Test()
        {
            var c = Criteria.SequenceLengthSameColour;
            var blueThree = CreateStandardCard(Colour.Blue, Symbol.Three);
            var blueFour = CreateStandardCard(Colour.Blue, Symbol.Four);
            var blueFive = CreateStandardCard(Colour.Blue, Symbol.Five);
            var greenFive = CreateStandardCard(Colour.Green, Symbol.Five);
            var greenSix = CreateStandardCard(Colour.Green, Symbol.Six);
            var greenSeven = CreateStandardCard(Colour.Green, Symbol.Seven);

            Assert.True(c(3)([blueThree, blueFour, blueFive]));
            Assert.True(c(2)([blueThree, blueFour, blueFive]));
            Assert.False(c(3)([blueThree, blueFour, greenFive]));
            Assert.True(c(2)([blueThree, blueFour, blueFive, greenSix, greenSeven]));
            Assert.False(c(5)([blueThree, blueFour, blueFive, greenSix, greenSeven]));
        }

        [Fact]
        public void ContainsRange_Test()
        {
            var c = Criteria.ContainsRange;
            var blueThree = CreateStandardCard(Colour.Blue, Symbol.Three);
            var blueFour = CreateStandardCard(Colour.Blue, Symbol.Four);
            var blueFive = CreateStandardCard(Colour.Blue, Symbol.Five);

            Assert.True(c([3, 4, 5])([blueThree, blueFour, blueFive]));
            Assert.True(c([3, 4])([blueThree, blueFour, blueFive]));
            Assert.False(c([3, 4, 5, 6])([blueThree, blueFour, blueFive]));
        }
    }
}

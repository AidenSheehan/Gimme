using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GimmeDigital
{
    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }

        public static Symbol AsSymbol(this int number) =>
            number switch
            {
                1 => Symbol.One,
                2 => Symbol.Two,
                3 => Symbol.Three,
                4 => Symbol.Four,
                5 => Symbol.Five,
                6 => Symbol.Six,
                7 => Symbol.Seven,
                _ => throw new ArgumentException("Number is not a valid symbol"),
            };

        public static Symbol AsSymbol(this string number) =>
            number.Trim().ToLower() switch
            {
                "one" or "1" => Symbol.One,
                "two" or "2" => Symbol.Two,
                "three" or "3" => Symbol.Three,
                "four" or "4" => Symbol.Four,
                "five" or "5" => Symbol.Five,
                "six" or "6" => Symbol.Six,
                "seven" or "7" => Symbol.Seven,
                "star" => Symbol.Star,
                _ => throw new ArgumentException("String is not a valid symbol"),
            };

        public static Colour AsColour(this string colour) =>
            colour.Trim().ToLower() switch
            {
                "red" => Colour.Red,
                "green" => Colour.Green,
                "blue" => Colour.Blue,
                _ => throw new ArgumentException("String is not a valid colour"),
            };

        public static bool IsNumber(this Symbol symbol) =>
            symbol switch
            {
                Symbol.Star => false,
                Symbol.Undefined => false,
                _ => true,
            };

        public static int AsNumber(this Symbol symbol) =>
            symbol switch
            {
                Symbol.One => 1,
                Symbol.Two => 2,
                Symbol.Three => 3,
                Symbol.Four => 4,
                Symbol.Five => 5,
                Symbol.Six => 6,
                Symbol.Seven => 7,
                _ => throw new ArgumentException("Symbol is not a valid number"),
            };

        public static IEnumerable<ICloneable> CreateCopies(this ICloneable cloneable, uint amount)
        {
            List<ICloneable> clones = [];
            for (int i = 0; i < amount; i++)
            {
                clones.Add((ICloneable)cloneable.Clone());
            }
            return clones;
        }
    }
}

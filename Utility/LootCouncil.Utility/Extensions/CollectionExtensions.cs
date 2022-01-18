using System;
using System.Collections.Generic;

namespace LootCouncil.Utility.Extensions
{
    public static class CollectionExtensions
    {
        private static readonly Random Rng = new();

        //Fisher-Yates Shuffle Algorithm
        public static IList<T> Shuffle<T>(this IList<T> collection)
        {
            var n = collection.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                (collection[k], collection[n]) = (collection[n], collection[k]);
            }

            return collection;
        }
    }
}
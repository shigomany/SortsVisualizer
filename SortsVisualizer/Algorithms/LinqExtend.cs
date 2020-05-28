using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SortsVisualizer.Algorithms
{
    public static class LinqExtend
    {
        private static Random rng = new Random();
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int parts)
        {
            return source.Select((x, i) => new { Index = i, Value = x })
                         .GroupBy(x => x.Index / parts)
                         .Select(x => x.Select(v => v.Value));
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

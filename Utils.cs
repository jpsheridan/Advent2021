using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using System.Threading.Tasks;

namespace AdventCode
{
    static class Utils
    {
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                yield break;
            }

            var list = sequence.ToList();

            if (!list.Any())
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var startingElementIndex = 0;

                foreach (var startingElement in list)
                {
                    var index = startingElementIndex;
                    var remainingItems = list.Where((e, i) => i != index);

                    foreach (var permutationOfRemainder in remainingItems.Permute())
                    {
                        yield return startingElement.Concat(permutationOfRemainder);
                    }

                    startingElementIndex++;
                }
            }
        }

        private static IEnumerable<T> Concat<T>(this T firstElement, IEnumerable<T> secondSequence)
        {
            yield return firstElement;
            if (secondSequence == null)
            {
                yield break;
            }

            foreach (var item in secondSequence)
            {
                yield return item;
            }
        }

        public static string[,] GetGridFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);

            
            string ln = "";
            int r = 0;
            int c = 0;

            ln = sr.ReadLine();
            r++;
            c = ln.Length;
            while ((ln = sr.ReadLine()) != null)
            {
                r++;
            }

            string[,] grid = new string[c+2, r+2];

            r = 0;
            c = 0;
            
            sr.BaseStream.Position = 0;

            while ((ln = sr.ReadLine()) != null)
            {
                r++;
                for (c = 0; c < ln.Length; c++)
                {
                    grid[c + 1, r] = ln.Substring(c, 1);
                }

            }

            return grid;
        }

        public static string[,,] Get3dGridFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);


            string ln = "";
            int r = 0;
            int c = 0;
            int z = 50;

            ln = sr.ReadLine();
            r++;
            c = ln.Length;

            while ((ln = sr.ReadLine()) != null)
            {
                r++;
            }

            string[,,] grid = new string[100, c + 2, r + 2];

            for (int w = 0; w < 100; w++)
            {
                for (int x = 0; x < c+1; x++)
                {
                    for (int y = 0; y < r + 1; y++)
                    {
                        grid[w, x, y] = ".";
                    }
                }
            }

            
            r = 0;
            c = 0;

            sr.BaseStream.Position = 0;

            while ((ln = sr.ReadLine()) != null)
            {
                r++;
                for (c = 0; c < ln.Length; c++)
                {
                    grid[z,c + 1, r] = ln.Substring(c, 1);
                }

            }

            return grid;
        }

       
    }
}

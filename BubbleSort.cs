using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public static class BubbleSort
    {
        public static void MakeBubbleSort(List<string> collection, Func<string, string, int> comparator = default)
        {
            if (comparator == null)
            {
                comparator = string.CompareOrdinal;
            }

            for (var i = 0; i < collection.Count; i++)
            {
                for (var j = 0; j < collection.Count - 1 - i; j++)
                {
                    if (comparator(collection[j], collection[j + 1]) > 0)
                    {
                        var temp = collection[j];
                        collection[j] = collection[j + 1];
                        collection[j + 1] = temp;
                    }
                }
            }
        }
    }
}

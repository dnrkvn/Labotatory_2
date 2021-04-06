using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public static class InsertionSort
    {
        public static void MakeInsertSort(List<string> collection, Func<string, string, int> comparator = default)
        {
            if (comparator == null)
            {
                comparator = string.CompareOrdinal;
            }

            for (var i = 1; i < collection.Count; i++)
            {
                var j = i;
                while (j > 0 && comparator(collection[j - 1], collection[j]) > 0)
                {
                    var temp = collection[j];
                    collection[j] = collection[j - 1];
                    collection[j - 1] = temp;
                    --j;
                }

                /*for (var j = i; j > 0 && comparator(collection[j - 1], collection[j]) > 0; --j)
                {
                    var temp = collection[j];
                    collection[j] = collection[j - 1];
                    collection[j - 1] = temp;
                }*/
            }
        }
    }
}

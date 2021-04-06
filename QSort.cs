using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public static class QSort
    {
        public static void MakeQuickSort(this List<string> collection, Func<string, string, int> comparator = default)
        {
            if (comparator == null)
            {
                comparator = string.CompareOrdinal;
            }
            MakeQuickSort(collection, 0, collection.Count - 1, comparator);
        }

        private static void MakeQuickSort(List<string> collection, int left, int right,
            Func<string, string, int> comparator = default)
        {
            var i = left;
            var j = right;
            var middle = collection[(left + right) / 2];

            while (i <= j)
            {
                while (comparator(collection[i], middle) < 0)
                {
                    i++;
                }
                while (comparator(collection[j], middle) > 0)
                {
                    j--;
                }

                if (i > j)
                {
                    continue;
                }

                var temp = collection[i];
                collection[i++] = collection[j];
                collection[j--] = temp;
            }

            if (left < j)
            {
                MakeQuickSort(collection, left, j, comparator);
            }
            if (i < right)
            {
                MakeQuickSort(collection, i, right, comparator);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public static class RadixSort
    {
        private static readonly int k = 256;  // Разрядность данных: количество возможных значений разряда ключа

        public static void MakeRadixSort(List<string> array)
        {
            MakeRadixSort(array, 0, array.Count - 1, 0, new string[array.Count]);
        }

        private static void MakeRadixSort(List<string> array, int left, int right, int d, string[] temp)
        {
            if (left >= right)
            {
                return;
            }

            var count = new int[k + 2];

            for (var i = left; i <= right; i++)
            {
                count[Key(array[i]) + 2]++;
            }

            for (var i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1];
            }

            for (var i = left; i <= right; i++)
            {
                temp[count[Key(array[i]) + 1]++] = array[i];
            }

            for (var i = left; i <= right; i++)
            {
                array[i] = temp[i - left];
            }

            for (var i = 0; i < k; i++)
            {
                MakeRadixSort(array, left + count[i], left + count[i + 1] - 1, d + 1, temp);
            }

            int Key(string s)
            {
                return d >= s.Length ? -1 : s[d];
            }
        }
    }
}

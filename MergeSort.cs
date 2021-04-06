using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public static class MergeSort
    {
        public static void MakeMergeSort(List<string> collection, Func<string, string, int> comparator = default)
        {
            if (comparator == null)
            {
                comparator = string.CompareOrdinal;
            }
            MakeMergeSort(collection, 0, collection.Count - 1, comparator);
        }

        private static void MakeMergeSort(List<string> collection, int lowIndex, int highIndex,
            Func<string, string, int> comparator = default)
        {
            if (lowIndex >= highIndex)
            {
                return;
            }

            var middleIndex = (lowIndex + highIndex) / 2;

            MakeMergeSort(collection, lowIndex, middleIndex, comparator);
            MakeMergeSort(collection, middleIndex + 1, highIndex, comparator);
            Merge(collection, lowIndex, middleIndex, highIndex, comparator);
        }

        // Метод для слияния массивов
        private static void Merge(List<string> collection, int lowIndex, int middleIndex, int highIndex,
            Func<string, string, int> comparator = default)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new string[highIndex - lowIndex + 1];
            var index = 0;

            while (left <= middleIndex && right <= highIndex)
            {
                tempArray[index++] = comparator(collection[left], collection[right]) < 0
                    ? collection[left++]
                    : collection[right++];
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index++] = collection[i];
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index++] = collection[i];
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                collection[lowIndex + i] = tempArray[i];
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public static class HeapSort
    {
        public static void MakeHeapSort(List<string> collection, Func<string, string, int> comparator = default)
        {
            if (comparator == null)
            {
                comparator = string.CompareOrdinal;
            }

            var size = collection.Count;

            for (var i = size / 2 - 1; i >= 0; i--) // Строим пирамиду 
            {
                Heapify(collection, size, i, comparator);
            }

            for (var i = size - 1; i >= 0; i--)
            {
                var temp = collection[0];
                collection[0] = collection[i];
                collection[i] = temp;

                Heapify(collection, i, 0, comparator); // Восстанавливаем пирамидальность a[0]...a[i-1]
            }
        }

        // Функция, которая перемещает элемент вниз по дереву для восстановления свойств дерева
        private static void Heapify(List<string> collection, int size, int index,
            Func<string, string, int> comparator = default)
        {
            while (true)
            {
                var largest = index;
                var left = 2 * index + 1;
                var right = 2 * index + 2;

                if (left < size && comparator(collection[left], collection[largest]) > 0)
                {
                    largest = left;
                }

                if (right < size && comparator(collection[right], collection[largest]) > 0)
                {
                    largest = right;
                }

                if (largest == index)
                {
                    return;
                }

                var temp = collection[index];
                collection[index] = collection[largest];
                collection[largest] = temp;

                index = largest;
            }
        }
    }
}

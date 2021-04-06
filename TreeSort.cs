using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    public static class TreeSort
    {
        public static List<string> MakeTreeSort(List<string> collection, Func<string, string, int> comparator = default)
        {
            if (comparator == null)
            {
                comparator = string.CompareOrdinal;
            }

            var treeNode = new TreeNode(collection[0]);
            for (var i = 1; i < collection.Count; i++)
            {
                treeNode.Add(new TreeNode(collection[i]), comparator);
            }
            return treeNode.Transform().ToList();
        }

        public class TreeNode
        {
            public TreeNode(string data)
            {
                Data = data;
            }

            public string Data { get; set; }
            public TreeNode Left { get; set; } // Левая ветка дерева
            public TreeNode Right { get; set; } // Правая ветка дерева

            // Рекурсивное добавление узла в дерево
            public void Add(TreeNode node, Func<string, string, int> comparator = default)
            {
                if (comparator == null)
                {
                    comparator = string.CompareOrdinal;
                }

                // Случай 1: Вставляемое значение меньше значения узла
                if (comparator(node.Data, Data) < 0)
                {
                    if (Left == null) // Если нет левого поддерева, добавляем значение в левого ребенка
                    {
                        Left = node;
                    }
                    else // В противном случае повторяем для левого поддерева
                    {
                        Left.Add(node, comparator);
                    }
                }

                // Случай 2: Вставляемое значение больше или равно значению узла
                else if (Right == null)
                {
                    Right = node;
                }
                else
                {
                    Right.Add(node, comparator);
                }
            }

            // Преобразование дерева в отсортированный массив
            public IEnumerable<string> Transform()
            {
                if (Left != null)
                {
                    foreach (var value in Left.Transform())
                    {
                        yield return value;
                    }
                }

                yield return Data;

                if (Right != null)
                {
                    foreach (var value in Right.Transform())
                    {
                        yield return value;
                    }
                }
            }
        }
    }
}

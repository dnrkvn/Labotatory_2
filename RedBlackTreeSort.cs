using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApp2
{
    public static class RedBlackTreeSort
    {
        public static List<string> MakeRBSort(List<string> collection)
        {
            var tree = new RedBlackTree<string>();
            foreach (var item in collection)
            {
                tree.Insert(item);
            }

            return tree.Select(n => n).ToList();
        }
    }
    internal class BSTEnumerator<T> : IEnumerator<T> where T : IComparable
    {
        private readonly bool asc;

        private readonly BSTNodeBase<T> root;
        private BSTNodeBase<T> current;

        internal BSTEnumerator(BSTNodeBase<T> root, bool asc = true)
        {
            this.root = root;
            this.asc = asc;
        }

        public bool MoveNext()
        {
            if (root == null)
            {
                return false;
            }

            if (current == null)
            {
                current = asc ? root.FindMin() : root.FindMax();
                return true;
            }

            var next = asc ? current.NextHigher() : current.NextLower();
            if (next != null)
            {
                current = next;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            current = root;
        }

        public T Current => current.Value;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            current = null;
        }
    }
    internal abstract class BSTNodeBase<T> where T : IComparable
    {
        internal int Count { get; set; } = 1;

        internal virtual BSTNodeBase<T> Parent { get; set; }
        internal virtual BSTNodeBase<T> Left { get; set; }
        internal virtual BSTNodeBase<T> Right { get; set; }

        internal T Value { get; set; }

        internal bool IsLeftChild => Parent.Left == this;
        internal bool IsRightChild => Parent.Right == this;
    }

    internal static class BSTExtensions
    {
        internal static BSTNodeBase<T> FindMax<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            if (node == null)
            {
                return null;
            }

            while (true)
            {
                if (node.Right == null)
                {
                    return node;
                }
                node = node.Right;
            }
        }

        internal static BSTNodeBase<T> FindMin<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            if (node == null)
            {
                return null;
            }

            while (true)
            {
                if (node.Left == null)
                {
                    return node;
                }
                node = node.Left;
            }
        }

        internal static BSTNodeBase<T> NextLower<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            if (node.Parent == null || node.IsLeftChild)
            {
                if (node.Left != null)
                {
                    node = node.Left;

                    while (node.Right != null)
                    {
                        node = node.Right;
                    }

                    return node;
                }

                while (node.Parent != null && node.IsLeftChild)
                {
                    node = node.Parent;
                }

                return node?.Parent;
            }

            if (node.Left != null)
            {
                node = node.Left;

                while (node.Right != null)
                {
                    node = node.Right;
                }

                return node;
            }

            return node.Parent;
        }

        internal static BSTNodeBase<T> NextHigher<T>(this BSTNodeBase<T> node) where T : IComparable
        {
            if (node.Parent == null || node.IsLeftChild)
            {
                if (node.Right != null)
                {
                    node = node.Right;

                    while (node.Left != null)
                    {
                        node = node.Left;
                    }

                    return node;
                }

                return node.Parent;
            }

            if (node.Right != null)
            {
                node = node.Right;

                while (node.Left != null)
                {
                    node = node.Left;
                }

                return node;
            }

            while (node.Parent != null && node.IsRightChild)
            {
                node = node.Parent;
            }

            return node?.Parent;
        }


        internal static void UpdateCounts<T>(this BSTNodeBase<T> node, bool spiralUp = false) where T : IComparable
        {
            while (node != null)
            {
                var leftCount = node.Left?.Count ?? 0;
                var rightCount = node.Right?.Count ?? 0;

                node.Count = leftCount + rightCount + 1;

                node = node.Parent;

                if (!spiralUp)
                    break;
            }
        }

        internal static int Position<T>(this BSTNodeBase<T> node, T item) where T : IComparable
        {
            if (node == null)
            {
                return -1;
            }

            var leftCount = node.Left?.Count ?? 0;

            if (node.Value.CompareTo(item) == 0)
            {
                return leftCount;
            }

            if (item.CompareTo(node.Value) < 0)
            {
                return Position(node.Left, item);
            }

            var position = Position(node.Right, item);

            return position < 0 ? position : position + leftCount + 1;
        }
    }

    internal class RedBlackTreeNode<T> : BSTNodeBase<T> where T : IComparable
    {
        internal new RedBlackTreeNode<T> Parent
        {
            get => (RedBlackTreeNode<T>)base.Parent;
            set => base.Parent = value;
        }

        internal new RedBlackTreeNode<T> Left
        {
            get => (RedBlackTreeNode<T>)base.Left;
            set => base.Left = value;
        }

        internal new RedBlackTreeNode<T> Right
        {
            get => (RedBlackTreeNode<T>)base.Right;
            set => base.Right = value;
        }

        internal RedBlackTreeNodeColor NodeColor { get; set; }

        internal RedBlackTreeNode<T> Sibling => Parent.Left == this ? Parent.Right : Parent.Left;

        internal RedBlackTreeNode(RedBlackTreeNode<T> parent, T value)
        {
            Parent = parent;
            Value = value;
            NodeColor = RedBlackTreeNodeColor.Red;
        }
    }
    public class RedBlackTree<T> : IEnumerable<T> where T : IComparable
    {
        internal RedBlackTreeNode<T> Root { get; set; }
        internal readonly Dictionary<T, BSTNodeBase<T>> nodeLookUp;

        public RedBlackTree(bool enableNodeLookUp = false, IEqualityComparer<T> equalityComparer = null)
        {
            if (enableNodeLookUp)
            {
                if (!typeof(T).GetTypeInfo().IsValueType && equalityComparer == null)
                {
                    throw new ArgumentException("equalityComparer parameter is required when node lookup us enabled and T is not a value type.");
                }

                nodeLookUp = new Dictionary<T, BSTNodeBase<T>>(equalityComparer ?? EqualityComparer<T>.Default);
            }
        }

        public int Insert(T value)
        {
            var node = InsertAndReturnNode(value);
            return node.Item2;
        }

        internal (RedBlackTreeNode<T>, int) InsertAndReturnNode(T value)
        {
            if (Root == null)
            {
                Root = new RedBlackTreeNode<T>(null, value) { NodeColor = RedBlackTreeNodeColor.Black };
                if (nodeLookUp != null)
                {
                    nodeLookUp[value] = Root;
                }

                return (Root, 0);
            }

            var newNode = Insert(Root, value);

            if (nodeLookUp != null)
            {
                nodeLookUp[value] = newNode.Item1;
            }

            return newNode;
        }

        private (RedBlackTreeNode<T>, int) Insert(RedBlackTreeNode<T> currentNode, T newNodeValue)
        {
            var insertionPosition = 0;

            while (true)
            {
                var compareResult = currentNode.Value.CompareTo(newNodeValue);

                if (compareResult < 0)
                {
                    insertionPosition += (currentNode.Left != null ? currentNode.Left.Count : 0) + 1;

                    if (currentNode.Right == null)
                    {
                        var node = currentNode.Right = new RedBlackTreeNode<T>(currentNode, newNodeValue);
                        BalanceInsertion(currentNode.Right);
                        return (node, insertionPosition);
                    }

                    currentNode = currentNode.Right;
                }
                else if (compareResult > 0)
                {
                    if (currentNode.Left == null)
                    {
                        var node = currentNode.Left = new RedBlackTreeNode<T>(currentNode, newNodeValue);
                        BalanceInsertion(currentNode.Left);
                        return (node, insertionPosition);
                    }

                    currentNode = currentNode.Left;
                }
                else throw new Exception("Item with same key exists");
            }
        }

        private void BalanceInsertion(RedBlackTreeNode<T> nodeToBalance)
        {

            while (true)
            {
                if (nodeToBalance == Root)
                {
                    nodeToBalance.NodeColor = RedBlackTreeNodeColor.Black;
                    break;
                }

                if (nodeToBalance.NodeColor == RedBlackTreeNodeColor.Red)
                {
                    if (nodeToBalance.Parent.NodeColor == RedBlackTreeNodeColor.Red)
                    {
                        if (nodeToBalance.Parent.Sibling != null && nodeToBalance.Parent.Sibling.NodeColor == RedBlackTreeNodeColor.Red)
                        {
                            nodeToBalance.Parent.Sibling.NodeColor = RedBlackTreeNodeColor.Black;
                            nodeToBalance.Parent.NodeColor = RedBlackTreeNodeColor.Black;

                            if (nodeToBalance.Parent.Parent != Root)
                            {
                                nodeToBalance.Parent.Parent.NodeColor = RedBlackTreeNodeColor.Red;
                            }

                            nodeToBalance.UpdateCounts();
                            nodeToBalance.Parent.UpdateCounts();
                            nodeToBalance = nodeToBalance.Parent.Parent;
                        }
                        else if (nodeToBalance.Parent.Sibling == null || nodeToBalance.Parent.Sibling.NodeColor == RedBlackTreeNodeColor.Black)
                        {
                            if (nodeToBalance.IsLeftChild && nodeToBalance.Parent.IsLeftChild)
                            {
                                var newRoot = nodeToBalance.Parent;
                                SwapColors(nodeToBalance.Parent, nodeToBalance.Parent.Parent);
                                RightRotate(nodeToBalance.Parent.Parent);

                                if (newRoot == Root)
                                    Root.NodeColor = RedBlackTreeNodeColor.Black;

                                nodeToBalance.UpdateCounts();
                                nodeToBalance = newRoot;
                            }
                            else if (nodeToBalance.IsLeftChild && nodeToBalance.Parent.IsRightChild)
                            {
                                RightRotate(nodeToBalance.Parent);

                                var newRoot = nodeToBalance;

                                SwapColors(nodeToBalance.Parent, nodeToBalance);
                                LeftRotate(nodeToBalance.Parent);

                                if (newRoot == Root)
                                    Root.NodeColor = RedBlackTreeNodeColor.Black;

                                nodeToBalance.UpdateCounts();
                                nodeToBalance = newRoot;
                            }
                            else if (nodeToBalance.IsRightChild && nodeToBalance.Parent.IsRightChild)
                            {
                                var newRoot = nodeToBalance.Parent;
                                SwapColors(nodeToBalance.Parent, nodeToBalance.Parent.Parent);
                                LeftRotate(nodeToBalance.Parent.Parent);

                                if (newRoot == Root)
                                    Root.NodeColor = RedBlackTreeNodeColor.Black;

                                nodeToBalance.UpdateCounts();
                                nodeToBalance = newRoot;
                            }
                            else if (nodeToBalance.IsRightChild && nodeToBalance.Parent.IsLeftChild)
                            {
                                LeftRotate(nodeToBalance.Parent);

                                var newRoot = nodeToBalance;

                                SwapColors(nodeToBalance.Parent, nodeToBalance);
                                RightRotate(nodeToBalance.Parent);

                                if (newRoot == Root)
                                    Root.NodeColor = RedBlackTreeNodeColor.Black;

                                nodeToBalance.UpdateCounts();
                                nodeToBalance = newRoot;
                            }
                        }
                    }
                }

                if (nodeToBalance.Parent != null)
                {
                    nodeToBalance.UpdateCounts();
                    nodeToBalance = nodeToBalance.Parent;
                    continue;
                }

                break;
            }

            nodeToBalance.UpdateCounts(true);
        }

        private void SwapColors(RedBlackTreeNode<T> node1, RedBlackTreeNode<T> node2)
        {
            var tmpColor = node2.NodeColor;
            node2.NodeColor = node1.NodeColor;
            node1.NodeColor = tmpColor;
        }

        private void RightRotate(RedBlackTreeNode<T> node)
        {
            var prevRoot = node;
            var leftRightChild = prevRoot.Left.Right;

            var newRoot = node.Left;

            prevRoot.Left.Parent = prevRoot.Parent;

            if (prevRoot.Parent != null)
            {
                if (prevRoot.Parent.Left == prevRoot)
                    prevRoot.Parent.Left = prevRoot.Left;
                else prevRoot.Parent.Right = prevRoot.Left;
            }

            newRoot.Right = prevRoot;
            prevRoot.Parent = newRoot;

            newRoot.Right.Left = leftRightChild;
            if (newRoot.Right.Left != null)
                newRoot.Right.Left.Parent = newRoot.Right;

            if (prevRoot == Root)
                Root = newRoot;

            newRoot.Left.UpdateCounts();
            newRoot.Right.UpdateCounts();
            newRoot.UpdateCounts();
        }

        private void LeftRotate(RedBlackTreeNode<T> node)
        {
            var prevRoot = node;
            var rightLeftChild = prevRoot.Right.Left;

            var newRoot = node.Right;

            prevRoot.Right.Parent = prevRoot.Parent;

            if (prevRoot.Parent != null)
            {
                if (prevRoot.Parent.Left == prevRoot)
                    prevRoot.Parent.Left = prevRoot.Right;
                else prevRoot.Parent.Right = prevRoot.Right;
            }

            newRoot.Left = prevRoot;
            prevRoot.Parent = newRoot;

            newRoot.Left.Right = rightLeftChild;
            if (newRoot.Left.Right != null)
                newRoot.Left.Right.Parent = newRoot.Left;

            if (prevRoot == Root)
                Root = newRoot;

            newRoot.Left.UpdateCounts();
            newRoot.Right.UpdateCounts();
            newRoot.UpdateCounts();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BSTEnumerator<T>(Root);
        }
    }

    internal enum RedBlackTreeNodeColor
    {
        Black,
        Red
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{
    public class RedBlackBST<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node<TKey, TValue> Root { get; set; }

        public int Count { get; protected set; }

        private static readonly bool RED = true;
        private static readonly bool BLACK = false;

        public RedBlackBST()
        {
            Root = null;
            Count = 0;
        }

        public bool IsEmpty() { return Count == 0; }

        /// <summary>
        /// Returns the value associated with the given key
        /// </summary>
        public TValue GetValue(TKey key)
        {
            return GetValue(Root, key);
        }

        private TValue GetValue(Node<TKey, TValue> node, TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            if (node == null) return default(TValue);
            int cmp = key.CompareTo(node.Key);

            if (cmp < 0) return GetValue(node.Left, key);
            else if (cmp > 0) return GetValue(node.Right, key);
            else return node.Value;
        }

        public bool Contains(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            return GetValue(key) != null;
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException();
            if (value == null) return; // HANDLE MORE HERE
            Root = Add(Root, key, value);
            Root.Color = BLACK;
            Count++;

            if (!IsBalanced()) Console.WriteLine("!!!Tree not balanced");
            if (!IsBST()) Console.WriteLine("!!!Tree not a Binary Search Tree");
        }

        private Node<TKey, TValue> Add(Node<TKey, TValue> node, TKey key, TValue value)
        {
            if (node == null) return new Node<TKey, TValue>(key, value, RED);
            int cmp = key.CompareTo(node.Key);

            if (cmp < 0) node.Left = Add(node.Left, key, value);
            else if (cmp > 0) node.Right = Add(node.Right, key, value);
            else node.Value = value;

            return Balance(node);
        }

        /// <summary>
        /// Return the smallest key in the BST
        /// </summary>
        public TKey Min()
        {
            if (Count == 0) throw new InvalidOperationException("BST is empty!");
            return Min(Root).Key;
        }

        private Node<TKey, TValue> Min(Node<TKey, TValue> node)
        {
            if (node.Left == null) return node;
            else return Min(node.Left);
        }

        /// <summary>
        /// Return the largest key in the BST
        /// </summary>
        public TKey Max()
        {
            if (Count == 0) throw new InvalidOperationException("BST is empty!");
            return Max(Root).Key;
        }

        private Node<TKey, TValue> Max(Node<TKey, TValue> node)
        {
            if (node.Right == null) return node;
            else return Max(node.Right);
        }

        /// <summary>
        /// Returns the height of the BST
        /// </summary>
        public int GetTreeDepth()
        {
            return GetDepth(Root);
        }

        private int GetDepth(Node<TKey, TValue> node)
        {
            if (node == null) return -1;
            return 1 + Math.Max(GetDepth(node.Left), GetDepth(node.Right));
        }

        /// <summary>
        /// Returns all keys in the BST
        /// </summary>
        public IEnumerable<TKey> GetKeys(TraversalMethod method)
        {
            return GetKeys(Root, method);
        }

        private IEnumerable<TKey> GetKeys(Node<TKey, TValue> node, TraversalMethod method)
        {
            IEnumerable<TKey> TraverseLeft = node.Left == null ? new TKey[0] : GetKeys(node.Left, method);
            IEnumerable<TKey> Self = new TKey[1] { node.Key };
            IEnumerable<TKey> TraverseRight = node.Right == null ? new TKey[0] : GetKeys(node.Right, method);

            switch (method)
            {
                case TraversalMethod.PreOrder:
                    return Self.Concat(TraverseLeft).Concat(TraverseRight);
                case TraversalMethod.InOrder:
                    return TraverseLeft.Concat(Self).Concat(TraverseRight);
                case TraversalMethod.PostOrder:
                    return TraverseLeft.Concat(TraverseRight).Concat(Self);
                default:
                    throw new ArgumentException();
            }
        }

        public enum TraversalMethod
        {
            PreOrder,
            InOrder,
            PostOrder
        }

        /// <summary>
        /// Is all left nodes are strictly smaller than node and all right nodes are strictly larger
        /// </summary>
        private bool IsBST()
        {
            return IsBST(Root, default(TKey), default(TKey));
        }

        private bool IsBST(Node<TKey, TValue> node, TKey min, TKey max)
        {
            if (node == null) return true;
            if (min != null && node.Key.CompareTo(min) <= 0) return false;
            if (max != null && node.Key.CompareTo(max) >= 0) return false;
            return IsBST(node.Left, min, node.Key) && IsBST(node.Right, node.Key, max);
        }

        /// <summary>
        /// A perfectly balanced 2-3 tree is one whose null linkes are all the same distance from the root
        /// </summary>
        private bool IsBalanced()
        {
            // Count number of black links from root to min node
            int blackCnt = 0;
            Node<TKey, TValue> node = Root;
            while (node != null)
            {
                if (!IsRed(node)) blackCnt++;
                node = node.Left;
            }

            return IsBalanced(Root, blackCnt);
        }

        private bool IsBalanced(Node<TKey, TValue> node, int blackCnt)
        {
            if (node == null) return blackCnt == 0;
            if (!IsRed(node)) blackCnt--;
            return IsBalanced(node.Left, blackCnt) && IsBalanced(node.Right, blackCnt);
        }

        #region UltilityMethods

        /// <summary>
        /// Restore red-black tree invariant
        /// </summary>
        private Node<TKey, TValue> Balance(Node<TKey, TValue> node)
        {
            if (IsRed(node.Right) && !IsRed(node.Left)) node = RotateLeft(node);
            if (IsRed(node.Left) && IsRed(node.Left.Left)) node = RotateRight(node);
            if (IsRed(node.Left) && IsRed(node.Right)) FlipColors(node);

            return node;
        }

        private Node<TKey, TValue> RotateLeft(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> x = node.Right;
            node.Right = x.Left;
            x.Left = node;
            // node could be red or black
            x.Color = node.Color;
            node.Color = RED;
            return x;
        }

        private Node<TKey, TValue> RotateRight(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> x = node.Left;
            node.Left = x.Right;
            x.Right = node;
            // node could be red or black
            x.Color = node.Color;
            node.Color = RED;
            return x;
        }

        // precondition: 2 children are red, node is black
        // postcondition: 2 children are black, node is red
        private void FlipColors(Node<TKey, TValue> node)
        {
            node.Left.Color = BLACK;
            node.Right.Color = BLACK;
            node.Color = RED;
        }

        private bool IsRed(Node<TKey, TValue> node)
        {
            if (node == null) return BLACK;
            return node.Color == RED;
        }
        #endregion

        private class Node<TKey, TValue> where TKey : IComparable<TKey>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node<TKey, TValue> Left { get; set; }
            public Node<TKey, TValue> Right { get; set; }
            public bool Color { get; set; }
            public Node(TKey key, TValue value, bool color)
            {
                Key = key;
                Value = value;
                Color = color;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RedBlackBST<string, int> redBlackBST = new RedBlackBST<string, int>();
            redBlackBST.Add("S", 1);
            redBlackBST.Add("E", 1);
            redBlackBST.Add("A", 1);
            redBlackBST.Add("R", 1);
            redBlackBST.Add("C", 1);
            redBlackBST.Add("H", 1);
            redBlackBST.Add("X", 1);
            redBlackBST.Add("L", 1);
            redBlackBST.Add("P", 1);
            redBlackBST.Add("M", 1);

            Console.WriteLine("Count: " + redBlackBST.Count);

            Console.WriteLine("In order traversal: ");
            foreach (var item in redBlackBST.GetKeys(RedBlackBST<string, int>.TraversalMethod.InOrder))
                Console.Write(item + " ");

            Console.WriteLine();
        }
    }
}

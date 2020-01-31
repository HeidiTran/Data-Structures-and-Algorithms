using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{

    public class BST<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node<TKey, TValue> Root { get; set; }

        public int Count { get; protected set; }

        public BST()
        {
            Root = null;
            Count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException();
            if (value == null) return; // HANDLE MORE HERE
            Root = Add(Root, key, value);
            Count++;
        }

        private Node<TKey, TValue> Add(Node<TKey, TValue> node, TKey key, TValue value)
        {
            if (node == null) return new Node<TKey, TValue>(key, value);
            int cmp = key.CompareTo(node.Key);

            if (cmp < 0) node.Left = Add(node.Left, key, value);
            else if (cmp > 0) node.Right = Add(node.Right, key, value);
            else node.Value = value;

            return node;
        }

        private class Node<TKey, TValue> where TKey : IComparable<TKey>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node<TKey, TValue> Left { get; set; }
            public Node<TKey, TValue> Right { get; set; }
            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        public IEnumerable<TValue> TraverseTree(TraversalMethod method)
        {
            return TraverseNode(Root, method);
        }

        private IEnumerable<TValue> TraverseNode(Node<TKey, TValue> node, TraversalMethod method)
        {
            IEnumerable<TValue> TraverseLeft = node.Left == null ? new TValue[0] : TraverseNode(node.Left, method);
            IEnumerable<TValue> Self = new TValue[1] { node.Value };
            IEnumerable<TValue> TraverseRight = node.Right == null ? new TValue[0] : TraverseNode(node.Right, method);

            switch (method)
            {
                case TraversalMethod.PreOrder:
                    return Self.Concat(TraverseLeft).Concat(TraverseRight);
                case TraversalMethod.InOrder:
                    return TraverseLeft.Concat(Self).Concat(TraverseRight);
                case TraversalMethod.PostOrder:
                    return TraverseRight.Concat(TraverseRight).Concat(Self);
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            
        }

    }
}

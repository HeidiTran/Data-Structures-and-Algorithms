using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExamplePerfectBalance();
        }

        private static void ExamplePerfectBalance()
        {
            Console.WriteLine("***ExamplePerfectBalanceKeyValuePairsList***");
            List<KeyValuePair<string, int>> keyValuePairs = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("H", 6),
                new KeyValuePair<string, int>("C", 3),
                new KeyValuePair<string, int>("A", 2),
                new KeyValuePair<string, int>("E", 1),
                new KeyValuePair<string, int>("S", 9),
                new KeyValuePair<string, int>("R", 8),
                new KeyValuePair<string, int>("X", 7)
            };

            Console.WriteLine("Input:");
            foreach (var item in keyValuePairs)
                Console.WriteLine("(" + item.Key + ", " + item.Value + ")");

            BST<string, int> bst = new BST<string, int>();
            PerfectBalance(bst, keyValuePairs);

            Console.WriteLine("\nIn Order Traversal: ");
            foreach (var item in bst.GetKeys(BST<string, int>.TraversalMethod.InOrder))
                Console.WriteLine("(" + item + ", " + bst.GetValue(item) + ")");

            string[] keysArr = new string[] { "H", "C", "A", "E", "S", "R", "X" };

            Console.WriteLine("\n***ExamplePerfectBalanceKeysArray***");
            Console.WriteLine("Input:");
            foreach (var item in keysArr)
                Console.WriteLine(item);

            BST<string, int> bst2 = new BST<string, int>();
            PerfectBalance(bst2, keysArr);

            Console.WriteLine("\nIn Order Traversal: ");
            foreach (var item in bst2.GetKeys(BST<string, int>.TraversalMethod.InOrder))
                Console.WriteLine("(" + item + ", " + bst2.GetValue(item) + ")");
        }

        /// <summary>
        ///  Inserts a set of key-value pairs into an initially empty BST such that the tree produced is equivalent to binary search, 
        ///  (The sequence of compares done in the search for any key in the BST is the same as the sequence of compares used by binary search for the same set of key-value pairs)
        /// </summary>
        public static void PerfectBalance(BST<string, int> bst, List<KeyValuePair<string, int>> input)
        {
            if (input.Count == 0) return;

            input.Sort((x, y) => x.Key.CompareTo(y.Key));
            AddToBSTPerfect(bst, input, 0, input.Count);
        }

        public static void AddToBSTPerfect(BST<string, int> bst, List<KeyValuePair<string, int>> input, int l, int r)
        {
            if (r - l < 1) return;
            else if (r - l == 1)
            {
                bst.Add(input[l].Key, input[l].Value);
                return;
            }

            int mid = (l + r) / 2;
            bst.Add(input[mid].Key, input[mid].Value);

            AddToBSTPerfect(bst, input, l, mid);
            AddToBSTPerfect(bst, input, mid + 1, r);
        }

        /// <summary>
        ///  Inserts a set of keys into an initially empty BST such that the tree produced is equivalent to binary search, 
        ///  (The sequence of compares done in the search for any key in the BST is the same as the sequence of compares used by binary search for the same set of keys)
        /// </summary>
        public static void PerfectBalance(BST<string, int> bst, string[] input)
        {
            if (input.Length == 0) return;

            Array.Sort(input);
            AddToBSTPerfect(bst, input, 0, input.Length);
        }

        public static void AddToBSTPerfect(BST<string, int> bst, string[] input, int l, int r)
        {
            if (r - l < 1) return;
            else if (r - l == 1)
            {
                bst.Add(input[l], l);
                return;
            }

            int mid = (l + r) / 2;
            bst.Add(input[mid], mid);

            AddToBSTPerfect(bst, input, l, mid);
            AddToBSTPerfect(bst, input, mid + 1, r);
        }
    }

    public class BST<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node<TKey, TValue> Root { get; set; }

        public int Count { get; protected set; }

        public BST()
        {
            Root = null;
            Count = 0;
        }

        public bool IsEmpty() { return Count == 0; }

        /// <summary>
        /// Returns the number of key-value pairs in this BST
        /// </summary>
        public int Size()
        {
            return Size(Root);
        }

        /// <summary>
        /// Returns the number of key-value pairs in BST rooted at node
        /// </summary>
        private int Size(Node<TKey, TValue> node)
        {
            if (node == null) return 0;
            return 1 + Size(node.Left) + Size(node.Right);
        }

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
            if (node == null) return default;
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

        /// <summary>
        /// Remove the specified key and associated value from the BST
        /// </summary>
        public void Delete(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            Root = Delete(Root, key);
            Count--;
        }

        private Node<TKey, TValue> Delete(Node<TKey, TValue> node, TKey key)
        {
            if (node == null) return null;
            int cmp = key.CompareTo(node.Key);

            if (cmp < 0) Delete(node.Left, key);
            else if (cmp > 0) Delete(node.Right, key);

            if (node.Right == null) return node.Left;
            if (node.Left == null) return node.Right;

            Node<TKey, TValue> temp = node;

            // Node becomes the min node of right branch
            node = Min(temp.Right);
            node.Right = DeleteMin(temp.Right);
            node.Left = temp.Left;

            return node;
        }

        /// <summary>
        /// Remove the smallest key and associated value from the BST
        /// </summary>
        public void DeleteMin()
        {
            if (IsEmpty()) throw new InvalidOperationException("BST is empty!");
            Root = DeleteMin(Root);
            Count--;
        }

        private Node<TKey, TValue> DeleteMin(Node<TKey, TValue> node)
        {
            if (node.Left == null) return node.Right;
            node.Left = DeleteMin(node.Left);
            return node;
        }

        /// <summary>
        /// Remove the largest key and associated value from the BST
        /// </summary>
        public void DeleteMax()
        {
            if (IsEmpty()) throw new InvalidOperationException("BST is empty!");
            Root = DeleteMax(Root);
            Count--;
        }

        private Node<TKey, TValue> DeleteMax(Node<TKey, TValue> node)
        {
            if (node.Right == null) return node.Left;
            node.Right = DeleteMax(node.Right);
            return node;
        }

        /// <summary>
        /// Return the smallest key in the BST
        /// </summary>
        public TKey Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("BST is empty!");
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
            if (IsEmpty()) throw new InvalidOperationException("BST is empty!");
            return Max(Root).Key;
        }

        private Node<TKey, TValue> Max(Node<TKey, TValue> node)
        {
            if (node.Right == null) return node;
            else return Max(node.Right);
        }

        /// <summary>
        /// Returns the largest key in the BST less than or equal to specified key
        /// </summary>
        public TKey Floor(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            if (IsEmpty()) throw new InvalidOperationException("BST is empty!");

            Node<TKey, TValue> res = Floor(Root, key);
            if (res == null) throw new InvalidOperationException("No key is less than or equal to specified key!");
            else return res.Key;
        }

        private Node<TKey, TValue> Floor(Node<TKey, TValue> node, TKey key)
        {
            if (node == null) return null;
            int cmp = key.CompareTo(node.Key);
            if (cmp == 0) return node;
            else if (cmp < 0) return Floor(node.Left, key);

            Node<TKey, TValue> floorRightBranchNode = Floor(node.Right, key);
            if (floorRightBranchNode == null) return node;
            else return floorRightBranchNode;
        }

        /// <summary>
        /// Returns the smallest key in the BST greater than or equal to specified key
        /// </summary>
        public TKey Ceilling(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            if (IsEmpty()) throw new InvalidOperationException("BST is empty!");

            Node<TKey, TValue> res = Ceilling(Root, key);
            if (res == null) throw new InvalidOperationException("No key is less than or equal to specified key!");
            else return res.Key;
        }

        private Node<TKey, TValue> Ceilling(Node<TKey, TValue> node, TKey key)
        {
            if (node == null) return null;
            int cmp = key.CompareTo(node.Key);
            if (cmp == 0) return node;
            else if (cmp > 0) return Floor(node.Right, key);

            Node<TKey, TValue> floorLeftBranchNode = Floor(node.Left, key);
            if (floorLeftBranchNode == null) return node;
            else return floorLeftBranchNode;
        }

        /// <summary>
        /// Returns the number of keys in the BST STRICTLY less than key
        /// </summary>
        public int Rank(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            return Rank(Root, key);
        }

        private int Rank(Node<TKey, TValue> node, TKey key)
        {
            if (node == null) return 0;
            int cmp = key.CompareTo(node.Key);
            if (cmp < 0) return Rank(node.Left, key);
            else if (cmp > 0) return 1 + Size(node.Left) + Rank(node.Right, key);
            else return Size(node.Left);
        }

        /// <summary>
        /// Return the key in the BST whose rank is k
        /// </summary>
        public TKey Select(int k)
        {
            if (k < 0 || k >= Count) throw new ArgumentException();

            Node<TKey, TValue> node = Select(Root, k);
            return node.Key;
        }

        private Node<TKey, TValue> Select(Node<TKey, TValue> node, int k)
        {
            if (node == null) return null;
            int t = Size(node.Left);

            if (k < t) return Select(node.Left, k);
            else if (k > t) return Select(node.Right, k - t - 1);
            else return node;
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

        /// <summary>
        /// Returns all keys in the BST in the given range
        /// </summary>
        public IEnumerable<TKey> GetKeys(TKey loKey, TKey hiKey)
        {
            if (loKey == null) throw new ArgumentNullException("First arg is null!");
            if (hiKey == null) throw new ArgumentNullException("First arg is null!");

            Queue<TKey> keys = new Queue<TKey>();
            GetKeys(Root, keys, loKey, hiKey);
            return keys;
        }

        private void GetKeys(Node<TKey, TValue> node, Queue<TKey> keys, TKey loKey, TKey hiKey)
        {
            if (node == null) return;
            int cmplo = loKey.CompareTo(node.Key);
            int cmphi = hiKey.CompareTo(node.Key);
            if (cmplo < 0) GetKeys(node.Left, keys, loKey, hiKey);
            if (cmplo <= 0 && cmphi >= 0) keys.Enqueue(node.Key);
            if (cmphi > 0) GetKeys(node.Right, keys, loKey, hiKey);
        }

        /// <summary>
        /// Returns all values in the BST
        /// </summary>
        public IEnumerable<TValue> GetValues(TraversalMethod method)
        {
            return GetValues(Root, method);
        }

        private IEnumerable<TValue> GetValues(Node<TKey, TValue> node, TraversalMethod method)
        {
            IEnumerable<TValue> TraverseLeft = node.Left == null ? new TValue[0] : GetValues(node.Left, method);
            IEnumerable<TValue> Self = new TValue[1] { node.Value };
            IEnumerable<TValue> TraverseRight = node.Right == null ? new TValue[0] : GetValues(node.Right, method);

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

        public IEnumerable<TKey> GetKeys2(TraversalMethod method)
        {
            if (IsEmpty()) return new Queue<TKey>();
            return GetKeys2(Min(), Max(), method);
        }

        private IEnumerable<TKey> GetKeys2(TKey lo, TKey hi, TraversalMethod method)
        {
            if (lo == null) throw new ArgumentNullException();
            if (hi == null) throw new ArgumentNullException();

            Queue<TKey> keys = new Queue<TKey>();
            GetKeys2(Root, keys, lo, hi, method);
            return keys;
        }

        private void GetKeys2(Node<TKey, TValue> node, Queue<TKey> keys, TKey lo, TKey hi, TraversalMethod method)
        {
            if (node == null) return;
            int cmplo = lo.CompareTo(node.Key);
            int cmphi = hi.CompareTo(node.Key);

            if (method == TraversalMethod.InOrder)
            {
                if (cmplo < 0) GetKeys2(node.Left, keys, lo, hi, method);
                if (cmplo <= 0 && cmphi >= 0) keys.Enqueue(node.Key);
                if (cmphi > 0) GetKeys2(node.Right, keys, lo, hi, method);
            }
            else if (method == TraversalMethod.PreOrder)
            {
                if (cmplo <= 0 && cmphi >= 0) keys.Enqueue(node.Key);
                if (cmplo < 0) GetKeys2(node.Left, keys, lo, hi, method);
                if (cmphi > 0) GetKeys2(node.Right, keys, lo, hi, method);
            }
            else if (method == TraversalMethod.PostOrder)
            {
                if (cmplo < 0) GetKeys2(node.Left, keys, lo, hi, method);
                if (cmphi > 0) GetKeys2(node.Right, keys, lo, hi, method);
                if (cmplo <= 0 && cmphi >= 0) keys.Enqueue(node.Key);
            }
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


        public enum TraversalMethod
        {
            PreOrder,
            InOrder,
            PostOrder
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
    }
}

using System;
using System.Collections.Generic;

namespace CsharpSample 
{
    /// <summary>
    /// A separate chaining Hash table
    /// </summary>
    class HashTableSepChaining<TKey, TValue>
    {
        private static readonly int INIT_CAPACITY = 3;
        private StackKV<TKey, TValue>[] KVStacks;
        /// <summary>
        /// Number of key-value pairs
        /// </summary>
        public int Count { get; protected set; }
        private int Size { get; set; } // HashTable size

        public HashTableSepChaining() : this(INIT_CAPACITY) { }

        public HashTableSepChaining(int size)
        {
            Size = size;
            KVStacks = new StackKV<TKey, TValue>[Size];
            for (int i = 0; i < Size; i++)
            {
                KVStacks[i] = new StackKV<TKey, TValue>();
            }
        }

        private int Hash(TKey key)
        {
            return (key.GetHashCode() & 0x7fffffff) % Size;
        }

        public bool Contains(TKey key)
        {
            if (key == null) throw new ArgumentNullException();

            try
            {
                GetValue(key);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public TValue GetValue(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            int index = Hash(key);
            return KVStacks[index].GetValue(key);
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException();
            if (value == null) return; // HANDLE MORE HERE: DELETE NODE With the key = key

            // Double the size of HashTable if avg length of list >= 10
            if (Count >= 10 * Size) Resize(2 * Size);

            int index = Hash(key);
            if (!KVStacks[index].Contains(key)) Count++;
            KVStacks[index].Add(key, value);
        }

        public void Remove(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            int index = Hash(key);
            if (!KVStacks[index].Contains(key)) Count--;
            KVStacks[index].Remove(key);

            if (Size > INIT_CAPACITY && Count <= 2 * Size) Resize(Size / 2);
        }

        /// <summary>
        /// Returns all keys in the HashTable
        /// </summary>
        public IEnumerable<TKey> GetKeys()
        {
            Queue<TKey> keys = new Queue<TKey>();

            for (int i = 0; i < Size; i++)
            {
                foreach (var key in KVStacks[i].GetKeys())
                    keys.Enqueue(key);
            }

            return keys;
        }

        private void Resize(int newSize)
        {
            HashTableSepChaining<TKey, TValue> temp = new HashTableSepChaining<TKey, TValue>(newSize);

            for (int i = 0; i < Size; i++)
            {
                foreach (var key in KVStacks[i].GetKeys())
                    temp.Add(key, GetValue(key));
            }

            Count = temp.Count;
            Size = temp.Size;
            KVStacks = temp.KVStacks;
        }
    }

    /// <summary>
    /// Stack of generic key-value pairs
    /// </summary>
    public class StackKV<TKey, TValue>
    {
        private Node Head { get; set; }
        public int Count { get; protected set; }

        public StackKV() { }

        public bool Contains(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            return Get(key) != null;
        }

        /// <summary>
        /// Returns the value associated with the given key
        /// </summary>
        public TValue GetValue(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            Node node = Get(key);
            if (node == null) throw new KeyNotFoundException();
            return node.Value;
        }

        private Node Get(TKey key)
        {
            if (key == null) throw new ArgumentNullException();

            for (Node node = Head; node != null; node = node.Next)
                if (key.Equals(node.Key))
                    return node;

            return null;
        }

        /// <summary>
        /// Inserts the Key-Value pair into the LinkedList, updates the value for the key if the key already exists.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException();
            if (value == null) return; // HANDLE MORE HERE: DELETE NODE With the key = key

            // If key already exists -> update the associated value
            for (Node node = Head; node != null; node = node.Next)
            {
                if (key.Equals(node.Key))
                {
                    node.Value = value;
                    return;
                }
            }

            Head = new Node(key, value, Head);
            Count++;
        }

        /// <summary>
        /// Removes the specified key and its associated value from the LinkedList
        /// </summary>
        public void Remove(TKey key)
        {
            if (!Contains(key)) return;
            Head = Remove(Head, key);
        }

        private Node Remove(Node node, TKey key)
        {
            if (node == null) return null;
            if (key.Equals(node.Key))
            {
                Count--;
                return node.Next;
            }

            node.Next = Remove(node.Next, key);
            return node;
        }

        /// <summary>
        /// Returns all keys in the LinkedList
        /// </summary>
        public IEnumerable<TKey> GetKeys()
        {
            Queue<TKey> keys = new Queue<TKey>();

            for (Node node = Head; node != null; node = node.Next)
                keys.Enqueue(node.Key);

            return keys;
        }

        /// <summary>
        /// Returns all values in the LinkedList
        /// </summary>
        public IEnumerable<TValue> GetValues()
        {
            Queue<TValue> values = new Queue<TValue>();

            for (Node node = Head; node != null; node = node.Next)
                values.Enqueue(node.Value);

            return values;
        }

        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Next { get; set; }

            public Node(TKey key, TValue value, Node next)
            {
                Key = key;
                Value = value;
                Next = next;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HashTableSepChaining<string, int> hashTable = new HashTableSepChaining<string, int>();
            hashTable.Add("S", 1);
            hashTable.Add("E", 2);
            hashTable.Add("A", 3);
            hashTable.Add("R", 4);
            hashTable.Add("C", 5);
            hashTable.Add("H", 6);

            Console.WriteLine("Count: " + hashTable.Count);
            Console.WriteLine("\nKeys: ");
            foreach (var k in hashTable.GetKeys())
                Console.Write(k + " ");
            Console.WriteLine("\nValues: ");
            foreach (var k in hashTable.GetKeys())
                Console.Write(hashTable.GetValue(k) + " ");

            Console.WriteLine();
            string key = "M";
            Console.WriteLine("\nContain key " + key + "? " + hashTable.Contains(key));
            key = "S";
            Console.WriteLine("\nContain key " + key + "? " + hashTable.Contains(key));
        }
    }
}

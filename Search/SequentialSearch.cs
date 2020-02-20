using System;
using System.Collections.Generic;

namespace CsharpSample 
{
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
            if (!Contains(key)) throw new KeyNotFoundException();
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
            StackKV<string, int> stackKV = new StackKV<string, int>();
            stackKV.Add("S", 1);
            stackKV.Add("E", 2);
            stackKV.Add("A", 3);
            stackKV.Add("R", 4);
            stackKV.Add("C", 5);
            stackKV.Add("H", 6);

            Console.WriteLine("Keys: ");
            foreach (var k in stackKV.GetKeys())
                Console.Write(k + " ");
            Console.WriteLine("\nValues: ");
            foreach (var v in stackKV.GetValues())
                Console.Write(v + " ");

            string key = "A";
            Console.WriteLine("\nValue of key " + key + ": " + stackKV.GetValue(key));
        }
    }
}

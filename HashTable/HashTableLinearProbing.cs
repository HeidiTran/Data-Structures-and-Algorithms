using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpSample
{
    /// <summary>
    /// A linear Probing Hash table
    /// DO NOT USE: Linear Probing deletion required a dummy value (that a user wouldn't search for) to place in. For non-nullable type t, it's not obvious what the dummy value could be. Thus, this code use default, which opens to pontential errors.
    /// DO NOT USE: In case key is int/long/etc. this would not work since Keys[index] would always != null
    /// </summary>
    public class HashTableLinearProb<TKey, TValue>
    {
        private static readonly int INIT_CAPACITY = 3;
        /// <summary>
        /// Number of key-value pairs
        /// </summary>
        public int Count { get; protected set; }
        private int Size { get; set; } // HashTable size
        private TKey[] Keys { get; set; }
        private TValue[] Values { get; set; }

        public HashTableLinearProb() : this(INIT_CAPACITY) { }

        public HashTableLinearProb(int capacity)
        {
            Count = 0;
            Size = capacity;
            Keys = new TKey[Size];
            Values = new TValue[Size];
        }

        private int Hash(TKey key)
        {
            return (key.GetHashCode() & 0x7fffffff) % Size;
        }

        public bool Contains(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            return GetValue(key) != null;
        }

        public object GetValue(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            int index = Hash(key);
            while (Keys[index] != null)
            {
                if (Keys[index].Equals(key))
                {
                    return Values[index];
                }
            }

            return null;
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException();
            if (value == null) return; // HANDLE MORE HERE: DELETE NODE With the key = key

            // Double the size of HashTable if 70% is full
            if (Count >= 0.7 * Size) Resize(2 * Size);

            int index = Hash(key);

            while (Keys[index] != null)
            {
                // If key already exists
                if (Keys[index].Equals(key))
                {
                    Values[index] = value;
                    return;
                }
                index = (index + 1) % Size;
            }

            Keys[index] = key;
            Values[index] = value;
            Count++;
        }

        public void Remove(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            if (!Contains(key)) return;

            int index = Hash(key);

            while (Keys[index] != null)
                index = (index + 1) % Size;

            Keys[index] = default;
            Values[index] = default;

            // Rehash all keys in same cluster
            index = (index + 1) % Size;
            while (Keys[index] != null)
            {
                TKey keyToRehash = Keys[index];
                TValue valueToRehash = Values[index];

                Keys[index] = default;
                Values[index] = default;
                Count--;
                Add(keyToRehash, valueToRehash);
                index = (index + 1) % Size;
            }

            Count--;

            // Halves the array's size if it's less than 12.5% full
            if (Count > 0 && Count < Size / 8) Resize(Size / 2);
        }

        public IEnumerable<TKey> GetKeys()
        {
            Queue<TKey> keys = new Queue<TKey>();
            for (int i = 0; i < Size; i++)
            {
                if (Keys[i] != null)
                    keys.Enqueue(Keys[i]);
            }

            return keys;
        }

        private void Resize(int newSize)
        {
            HashTableLinearProb<TKey, TValue> temp = new HashTableLinearProb<TKey, TValue>(newSize);

            for (int i = 0; i < Size; i++)
            {
                if (Keys[i] != null)
                    temp.Add(Keys[i], Values[i]);
            }

            Keys = temp.Keys;
            Values = temp.Values;
            Size = temp.Size;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HashTableLinearProb<string, int> hashTable = new HashTableLinearProb<string, int>();
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpSample
{
	public class HashTableBinarySearch<TKey, TValue> where TKey : IComparable<TKey>
	{
		private static readonly ulong INIT_CAPACITY = 2;

		/// <summary>
		/// Number of key-value pairs
		/// </summary>
		public int Count { get; protected set; }
		private TKey[] Keys { get; set; }
		private TValue[] Values { get; set; }

		public HashTableBinarySearch() : this(INIT_CAPACITY) { }
		public HashTableBinarySearch(ulong capacity)
		{
			Count = 0;
			Keys = new TKey[capacity];
			Values = new TValue[capacity];
		}

		private int Rank(TKey key)
		{
			if (key == null) throw new ArgumentNullException();
			int lo = 0, hi = Count - 1;

			while (lo <= hi)
			{
				int mid = lo + (hi - lo) / 2;
				int cmp = key.CompareTo(Keys[mid]);
				if (cmp < 0) hi = mid - 1;
				else if (cmp > 0) lo = mid + 1;
				else return mid;
			}

			return lo;
		}

		public bool Contains(TKey key)
		{
			if (key == null) throw new ArgumentNullException();
			return GetValue(key) != null;
		}

		public object GetValue(TKey key)
		{
			if (key == null) throw new ArgumentNullException();
			if (Count == 0) return null;
			int index = Rank(key);
			if (index < Count && Keys[index].CompareTo(key) == 0) return Values[index];
			return null;
		}

		public void Add(TKey key, TValue value)
		{
			if (key == null) throw new ArgumentNullException();
			if (value == null) return; // HANDLE MORE HERE: DELETE NODE With the key = key

			int index = Rank(key);

			// If key already exists, update the value
			if (index < Count && Keys[index].CompareTo(key) == 0)
			{
				Values[index] = value;
				return;
			}

			// If hash table is full
			if (Count == Keys.Count()) Resize(2 * Count);

			// Move forward by 1 to make space for new element
			for (int j = Count; j > index; j--)
			{
				Keys[j] = Keys[j - 1];
				Values[j] = Values[j - 1];
			}

			Keys[index] = key;
			Values[index] = value;
			Count++;
		}

		public void Remove(TKey key)
		{
			if (key == null) throw new ArgumentNullException();
			if (!Contains(key)) return;

			int index = Rank(key);

			// Move backward by 1 (Opposite of Add)
			for (int j = index; j < Count - 1; j++)
			{
				Keys[j] = Keys[j + 1];
				Values[j] = Values[j + 1];
			}

			Count--;
			Keys[Count] = default;
			Values[Count] = default;

			// If less than 1/4 is full, then resize down
			if (Count > 0 && Count < Keys.Length / 4) Resize(Keys.Length / 2);
		}

		/// <summary>
		/// Returns the largest key in the Hash Table less than or equal to specified key
		/// </summary>
		public TKey Floor(TKey key)
		{
			if (key == null) throw new ArgumentNullException();
			if (Count == 0) throw new InvalidOperationException("Hash Table is empty!");

			int index = Rank(key);

			// If Hash Table contains Key
			if (Contains(key)) return Keys[index];

			return Keys[index - 1];
		}

		/// <summary>
		/// Returns the smallest key in the Hash Table greater than or equal to specified key
		/// </summary>
		public TKey Ceilling(TKey key)
		{
			if (key == null) throw new ArgumentNullException();
			if (Count == 0) throw new InvalidOperationException("Hash Table is empty!");

			int index = Rank(key);

			if (index == Count) return default;

			return Keys[index];
		}

		/// <summary>
		/// Return the key in the Hash Table in which rank is k (k items is smaller than this one)
		/// </summary>
		public TKey Select(int k)
		{
			if (k < 0 || k > Count) throw new InvalidOperationException("Invalid argument!");

			return Keys[k];
		}

		public TKey Min() { return Keys[0]; }

		public TKey Max()
		{
			if (Count == 0) throw new InvalidOperationException("Hash Table is empty!");
			return Keys[Count - 1];
		}

		public IEnumerable<TKey> GetKeys()
		{
			return GetKeys(Min(), Max());
		}

		public IEnumerable<TKey> GetKeys(TKey lo, TKey hi)
		{
			if (lo == null) throw new ArgumentNullException();
			if (hi == null) throw new ArgumentNullException();

			Queue<TKey> keys = new Queue<TKey>();

			if (lo.CompareTo(hi) > 0) return keys;

			for (int i = Rank(lo); i < Rank(hi); i++)
				keys.Enqueue(Keys[i]);

			if (Contains(hi)) keys.Enqueue(Keys[Rank(hi)]);
			return keys;
		}

		private void Resize(int newSize)
		{
			if (newSize < Count) return;

			TKey[] tempKeys = new TKey[newSize];
			TValue[] tempValues = new TValue[newSize];

			for (int i = 0; i < Count; i++)
			{
				tempKeys[i] = Keys[i];
				tempValues[i] = Values[i];
			}

			Keys = tempKeys;
			Values = tempValues;
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
			HashTableBinarySearch<string, int> hashTable = new HashTableBinarySearch<string, int>();

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

			Console.WriteLine("Count: " + hashTable.Count);

			string key = "M";
			Console.WriteLine("\nContain key " + key + "? " + hashTable.Contains(key));
			key = "H";
			Console.WriteLine("\nContain key " + key + "? " + hashTable.Contains(key));

			Console.WriteLine("\nRemove " + key);
			hashTable.Remove(key);
			Console.WriteLine("Keys: ");
			foreach (var k in hashTable.GetKeys())
				Console.Write(k + " ");
			Console.WriteLine();
		}
	}
}

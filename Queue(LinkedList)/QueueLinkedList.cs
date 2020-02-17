using System;
using System.Collections;
using System.Collections.Generic;

namespace CsharpSample
{
	public class QueueLinkedList<T> : IEnumerable<T>
	{
		private LinkedList<T> List { get; set; }

		public QueueLinkedList()
		{
			List = new LinkedList<T>();
		}

		public void Enqueue(T item)
		{
			List.AddLast(item);
		}

		public T Dequeue()
		{
			if (IsEmpty()) 
				throw new InvalidOperationException("Empty Queue!");

			T val = List.First.Value;
			List.RemoveFirst();

			return val;
		}

		public bool IsEmpty()
		{
			return List.Count == 0;
		}

		public int Size()
		{
			return List.Count;
		}

		public T Peek()
		{
			if (IsEmpty())
				throw new InvalidOperationException("Empty Queue!");

			T val = List.First.Value;
			return val;
		}

		public void Clear()
		{
			List.Clear();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return List.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return List.GetEnumerator();
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
		}
	}
}


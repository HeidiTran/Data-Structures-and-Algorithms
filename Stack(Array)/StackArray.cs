using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpSample
{
    public class StackArray<T> : IEnumerable<T>
    {
        T[] arr = new T[0];
        int size;

        public void Push(T val)
        {
            // Array reached init size limit
            if (size == arr.Length)
            {
                int newSize = size == 0 ? 4 : size * 2;
                T[] newArr = new T[newSize];
                arr.CopyTo(newArr, 0);
                arr = newArr;
            }
            arr[size] = val;
            size++;
        }

        public T Pop()
        {
            if (size == 0)
                throw new InvalidOperationException("Empty Stack!");

            size--;
            return arr[size];
        }

        public T Peek()
        {
            if (size == 0)
                throw new InvalidOperationException("Empty Stack!");

            return arr[size - 1];
        }

        public void Clear()
        {
            size = 0;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = size - 1; i >= 0; i--)
            {
                yield return arr[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return arr.GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

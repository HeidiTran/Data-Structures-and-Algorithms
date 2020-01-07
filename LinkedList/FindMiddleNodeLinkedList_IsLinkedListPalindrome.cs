using System;
using System.Collections.Generic;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleCheckLinkedListIsPalindrome();
        }

        private static void ExampleCheckLinkedListIsPalindrome()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 4, 3, 2, 1 };
            LinkedList<int> linkedList = new LinkedList<int>(numbers);
            if (linkedList.Count == 0)
                throw new InvalidOperationException("Empty LinkedList!");

            foreach (var item in linkedList)
                Console.Write(item + " ");

            Console.WriteLine();
            if (IsLinkedListPalindrome(linkedList))
            {
                Console.WriteLine("LinkedList is a Palindrome!");
            }
            else
            {
                Console.WriteLine("LinkedList is NOT a Palindrome!");
            }
        }

        private static LinkedListNode<int> GetMiddleNode(LinkedList<int> linkedList)
        {
            if (linkedList.Count == 0)
                throw new InvalidOperationException("Empty LinkedList!");

            LinkedListNode<int> slowPointer = linkedList.First;
            LinkedListNode<int> fastPointer = linkedList.First;

            // Find the middle node
            while (fastPointer.Next != null && fastPointer.Next.Next != null)
            {
                fastPointer = fastPointer.Next.Next;
                slowPointer = slowPointer.Next;
            }

            return slowPointer;
        }
        private static bool IsLinkedListPalindrome(LinkedList<int> linkedList)
        {
            LinkedListNode<int> middleNode = GetMiddleNode(linkedList);

            if (linkedList.Count % 2 == 0)
                middleNode = middleNode.Next;

            // Push all nodes from the middle node to the end onto a stack
            Stack<int> tempStack = new Stack<int>();

            while (middleNode != null)
            {
                tempStack.Push(middleNode.Value);
                middleNode = middleNode.Next;
            }

            LinkedListNode<int> node = linkedList.First;
            while (tempStack.Count != 0 && node != middleNode)
            {
                if (tempStack.Pop() != node.Value)
                    return false;

                node = node.Next;
            }

            return true;
        }
    }
}

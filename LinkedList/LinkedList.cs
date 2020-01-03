using System;

namespace CsharpSample 
{
    public class Node
    {
        public int data;
        public Node next;
        public Node(int d)
        {
            data = d;
            next = null;
        }
    }

    public class DNode
    {
        public int data;
        public DNode next;
        public DNode prev;
        public DNode(int d)
        {
            data = d;
            next = null;
            prev = null;
        }
    }

    public class SingleLinkedList
    {
        public Node head;
    }

    public class DoubleLinkedList
    {
        public DNode head;
    }

    class Program
    {
        static void Main(string[] args)
        {
            ExampleSortedLinkedList();
        }

        private static SingleLinkedList ConstructSingleLinkedList(int size)
        {
            if (size < 1)
            {
                Console.WriteLine("Size can't be < 1");
                // handle here
            }

            SingleLinkedList singleLinkedList = new SingleLinkedList
            {
                head = new Node(0)
            };

            Node temp = singleLinkedList.head;
            for (int i = 1; i < size; i++)
            {
                temp.next = new Node(0);
                temp = temp.next;
            }

            return singleLinkedList;
        }

        private static SingleLinkedList SortSingleLinkedList(SingleLinkedList singleLinkedListA)
        {
            SingleLinkedList singleLinkedListB = ConstructSingleLinkedList(1);

            Node curNodeListA = singleLinkedListA.head.next;
            Node nextNodeListA = singleLinkedListA.head.next;
            Node tempListB = singleLinkedListB.head;
            while (curNodeListA != null)
            {
                nextNodeListA = curNodeListA.next;

                // Search through list B to find node temp with tempListB.next.data > curNodeListA.data
                // Insert curNodeListA into B after tempListB Node 
                // This iteration will exhaust List A and have sorted Nodes in List B
                while (tempListB.next != null)
                {
                    if (tempListB.next.data > curNodeListA.data)
                        break;

                    tempListB = tempListB.next;
                }

                curNodeListA.next = tempListB.next;
                tempListB.next = curNodeListA;
                curNodeListA = nextNodeListA;
            }

            return singleLinkedListB;
        }

        public static void ReverseSingleLinkedList(SingleLinkedList singleLinkedList)
        {
            Node prev = null;
            Node current = singleLinkedList.head;
            Node temp = singleLinkedList.head;

            while (current != null)
            {
                temp = current.next;
                current.next = prev;
                prev = current;
                current = temp;
            }

            singleLinkedList.head = prev;
        }

        public static void TraverseSingleLinkedList(SingleLinkedList singleLinkedList)
        {
            Node temp = singleLinkedList.head;

            Console.Write(temp.data + " -> ");

            while (temp.next != null)
            {
                temp = temp.next;
                Console.Write(temp.data + " -> ");
            }

            Console.WriteLine("null");
        }

        public static void InsertAfterSingleLinkedList(Node prevNode, Node node)
        {
            if (prevNode == null)
            {
                Console.WriteLine("Previous node can't be null");
                return;
            }

            node.next = prevNode.next;
            prevNode.next = node;
        }

        public static void InsertAfterDoubleLinkedList(DNode prevNode, DNode node)
        {
            if (prevNode == null)
            {
                Console.WriteLine("Previous node can't be null");
                return;
            }

            node.next = prevNode.next;
            prevNode.next.prev = node;
            prevNode.next = node;
            node.prev = prevNode;
        }

        public static void InsertBeforeDoubleLinkedList(DNode nextNode, DNode node)
        {
            if (nextNode == null)
            {
                Console.WriteLine("Previous node can't be null");
                return;
            }

            node.prev = nextNode.prev.next;
            nextNode.prev.next = node;
            node.next = nextNode;
            nextNode.prev = node;
        }

        public static void RemoveByValue(SingleLinkedList singleLinkedList, int val)
        {
            Node node = new Node(val);
            Node temp = singleLinkedList.head;

            if (temp != null && temp.data == val)
            {
                singleLinkedList.head = temp.next;
                return;
            }

            Node prev = null;
            while (temp != null && temp.data != val)
            {
                prev = temp;
                temp = temp.next;
            }

            if (temp == null)
                return;

            prev.next = temp.next;
        }

        public static void ExampleReverseSingleLinkedList()
        {
            const int N = 10;
            SingleLinkedList singleLinkedList = new SingleLinkedList
            {
                head = new Node(0)
            };

            Node temp = singleLinkedList.head;

            for (int i = 1; i < N; i++)
                temp = (temp.next = new Node(i));

            TraverseSingleLinkedList(singleLinkedList);
            ReverseSingleLinkedList(singleLinkedList);
            TraverseSingleLinkedList(singleLinkedList);
        }

        private static void ExampleSortedLinkedList()
        {
            const int N = 100;
            SingleLinkedList singleLinkedListA = new SingleLinkedList
            {
                head = new Node(0)
            };

            // Creates a linked list with random int between 1 and N
            Node temp = singleLinkedListA.head;
            Random rnd = new Random();
            for (int i = 0; i < N; i++)
                temp = (temp.next = new Node(rnd.Next(1, N)));

            TraverseSingleLinkedList(singleLinkedListA);

            SingleLinkedList orderedLinkedListA = SortSingleLinkedList(singleLinkedListA);

            Console.WriteLine("\nSorted Linked List: ");
            TraverseSingleLinkedList(orderedLinkedListA);
        }
    }
}


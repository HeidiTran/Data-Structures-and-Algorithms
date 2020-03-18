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

    public class SingleLinkedList
    {
        public Node head;
    }

    class Program
    {
        static void Main(string[] args)
        {
            ExampleDetectCycleLinkedList0();
            Console.WriteLine();
            ExampleDetectCycleLinkedList1();
            Console.WriteLine();
            ExampleDetectCycleLinkedList2();
            Console.WriteLine();
            ExampleDetectCycleLinkedList3();
            Console.WriteLine();
            ExampleDetectCycleLinkedList4();
        }

        private static void ExampleDetectCycleLinkedList0()
        {
            const int N = 2;

            SingleLinkedList linkedList = new SingleLinkedList
            {
                head = new Node(0)
            };

            Node temp = linkedList.head;
            for (int i = 1; i < N; i++)
                temp = (temp.next = new Node(i));

            TraverseSingleLinkedList(linkedList);

            if (ContainCycle(linkedList))
                Console.WriteLine("This linked list contains a cycle!");
            else
                Console.WriteLine("No cycle found!");
        }

        private static void ExampleDetectCycleLinkedList1()
        {
            const int N = 3;

            SingleLinkedList linkedList = new SingleLinkedList
            {
                head = new Node(0)
            };

            Node temp = linkedList.head;
            for (int i = 1; i < N; i++)
                temp = (temp.next = new Node(i));

            TraverseSingleLinkedList(linkedList);

            if (ContainCycle(linkedList))
                Console.WriteLine("This linked list contains a cycle!");
            else
                Console.WriteLine("No cycle found!");
        }

        private static void ExampleDetectCycleLinkedList4()
        {
            Console.WriteLine("0 -> 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7 -----");
            Console.WriteLine("                                   ^     /");
            Console.WriteLine("                                   ------");

            const int N = 8;
            SingleLinkedList linkedList = new SingleLinkedList
            {
                head = new Node(0)
            };

            Node temp = linkedList.head;
            for (int i = 1; i < N; i++)
                temp = (temp.next = new Node(i));

            Node t = linkedList.head;
            while (t.next != null)
                t = t.next;
            t.next = t;

            // UNCOMMENT the following line will fall into an infinite loop !!!
            //TraverseSingleLinkedList(linkedList);

            if (ContainCycle(linkedList))
                Console.WriteLine("This linked list contains a cycle!");
            else
                Console.WriteLine("No cycle found!");
        }

        private static void ExampleDetectCycleLinkedList3()
        {
            const int N = 8;
            SingleLinkedList linkedList = new SingleLinkedList
            {
                head = new Node(0)
            };

            Console.WriteLine(" 0 -> 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7");
            Console.WriteLine("           ^                       /");
            Console.WriteLine("           \\                      /");
            Console.WriteLine("            \\---------------------");
            Node temp = linkedList.head;
            for (int i = 1; i < N; i++)
                temp = (temp.next = new Node(i));

            Node t = linkedList.head;
            while (t.next != null)
                t = t.next;
            t.next = linkedList.head.next.next;

            // UNCOMMENT the following line will fall into an infinite loop !!!
            //TraverseSingleLinkedList(linkedList);

            if (ContainCycle(linkedList))
                Console.WriteLine("This linked list contains a cycle!");
            else
                Console.WriteLine("No cycle found!");
        }

        private static void ExampleDetectCycleLinkedList2()
        {
            const int N = 8;
            SingleLinkedList linkedList = new SingleLinkedList
            {
                head = new Node(0)
            };

            Console.WriteLine("0 -> 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7");
            Console.WriteLine("^                                  /");
            Console.WriteLine("\\                                 /");
            Console.WriteLine(" \\--------------------------------");
            Node temp = linkedList.head;
            for (int i = 1; i < N; i++)
                temp = (temp.next = new Node(i));

            Node t = linkedList.head;
            while (t.next != null)
                t = t.next;
            t.next = linkedList.head;

            // UNCOMMENT the following line will fall into an infinite loop !!!
            //TraverseSingleLinkedList(linkedList);

            if (ContainCycle(linkedList))
                Console.WriteLine("This linked list contains a cycle!");
            else
                Console.WriteLine("No cycle found!");
        }

        private static bool ContainCycle(SingleLinkedList singleLinkedListA)
        {
            Node slowPointer = singleLinkedListA.head;
            Node fastPointer = singleLinkedListA.head;

            while (fastPointer != null && fastPointer.next != null)
            {
                slowPointer = slowPointer.next;
                fastPointer = fastPointer.next.next;

                if (slowPointer == fastPointer)
                    return true;
            }

            return false;
        }

        public static SingleLinkedList ConstructSingleLinkedList(int size)
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
    }
}

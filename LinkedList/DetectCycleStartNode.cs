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
            ExampleDetectCycleStartNodeLinkedList1();
            Console.WriteLine();
            ExampleDetectCycleStartNodeLinkedList2();
            Console.WriteLine();
            ExampleDetectCycleStartNodeLinkedList3();
        }

        private static void ExampleDetectCycleStartNodeLinkedList3()
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

            Node cycleStartNode = new Node(0);
            if (CycleStartNode(linkedList, out cycleStartNode))
            {
                Console.WriteLine("This linked list contains a cycle!");
                Console.WriteLine("Value of cycleStartNode: " + cycleStartNode.data);
            }
            else
                Console.WriteLine("No cycle found!");
        }

        private static void ExampleDetectCycleStartNodeLinkedList2()
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

            Node cycleStartNode = new Node(0);
            if (CycleStartNode(linkedList, out cycleStartNode))
            {
                Console.WriteLine("This linked list contains a cycle!");
                Console.WriteLine("Value of cycleStartNode: " + cycleStartNode.data);
            }
            else
                Console.WriteLine("No cycle found!");
        }

        private static void ExampleDetectCycleStartNodeLinkedList1()
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

            Node cycleStartNode = new Node(0);
            if (CycleStartNode(linkedList, out cycleStartNode))
            {
                Console.WriteLine("This linked list contains a cycle!");
                Console.WriteLine("Value of cycleStartNode: " + cycleStartNode.data);
            }
            else
                Console.WriteLine("No cycle found!");
        }


        private static bool CycleStartNode(SingleLinkedList singleLinkedListA, out Node startNode)
        {
            bool res = false;
            Node slowPointer = singleLinkedListA.head;
            Node fastPointer = singleLinkedListA.head;

            while (fastPointer.next != null)
            {
                slowPointer = slowPointer.next;
                fastPointer = fastPointer.next.next;

                if (slowPointer == fastPointer)
                {
                    res = true;
                    break;
                }
            }

            // The distance from the first Node to the Node where the cycle starts = the distance from
            // where the 2 pointers met to the Node where the cycle starts
            slowPointer = singleLinkedListA.head;
            while (slowPointer != fastPointer)
            {
                slowPointer = slowPointer.next;
                fastPointer = fastPointer.next;
            }

            startNode = slowPointer;

            return res;
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


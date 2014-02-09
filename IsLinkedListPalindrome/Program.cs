using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsLinkedListPalindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<char> list = new LinkedList<char>();
            list.AddLast('r');
            list.AddLast('a');
            list.AddLast('d');
            list.AddLast('a');
            list.AddLast('r');

            Node node1 = new Node('r');
            Node node2 = new Node('a');
            Node node3 = new Node('d');
            Node node4 = new Node('a');
            Node node5 = new Node('r');

            node1.Next = node2;
            node2.Next = node3;
            node3.Next = node4;
            node4.Next = node5;

            Console.WriteLine("For Radar");
            Console.WriteLine("Test case 1 : {0}", IsPalindrome1(list));
            Console.WriteLine("Test case 2 : {0}", IsPalindrome2(node1));
            Console.WriteLine("Test case 3 : {0}", IsPalindrome3(node1));
            Console.WriteLine("Test case 4 : {0}", IsPalindrome4(node1));
            Console.WriteLine("Test case 5 : {0}", IsPalindrome5(node1));

            Console.WriteLine("For abba");
            LinkedList<char> list2 = new LinkedList<char>();
            list2.AddLast('a');
            list2.AddLast('b');
            list2.AddLast('b');
            list2.AddLast('a');

            Node testNode2 = new Node('a');
            testNode2.Next = new Node('b');
            testNode2.Next.Next = new Node('b');
            testNode2.Next.Next.Next = new Node('a');

            Console.WriteLine("Test case 1 : {0}", IsPalindrome1(list2));
            Console.WriteLine("Test case 2 : {0}", IsPalindrome2(testNode2));
            Console.WriteLine("Test case 3 : {0}", IsPalindrome3(testNode2));
            Console.WriteLine("Test case 4 : {0}", IsPalindrome4(testNode2));
            Console.WriteLine("Test case 5 : {0}", IsPalindrome5(testNode2));

            Console.WriteLine("For abc");

            LinkedList<char> list3 = new LinkedList<char>();
            list3.AddLast('a');
            list3.AddLast('b');
            list3.AddLast('c');

            Node testNode3 = new Node('a');
            testNode3.Next = new Node('b');
            testNode3.Next.Next = new Node('c');


            Console.WriteLine("Test case 1 : {0}", IsPalindrome1(list3));
            Console.WriteLine("Test case 2 : {0}", IsPalindrome2(testNode3));
            Console.WriteLine("Test case 3 : {0}", IsPalindrome3(testNode3));
            Console.WriteLine("Test case 4 : {0}", IsPalindrome4(testNode3));
            Console.WriteLine("Test case 5 : {0}", IsPalindrome5(testNode3));
        }


        // 1. Using a double linked list
        static bool IsPalindrome1(LinkedList<char> list)
        {
            LinkedListNode<char> first = list.First;
            LinkedListNode<char> last = list.Last;

            while (first != null && last != null)
            {
                if (
                    (first.Value.Equals(last.Value)) && 
                    ((first == last) || first.Next == last)
                   )
                {
                    return true;
                }
                else if (first.Value.Equals(last.Value))
                {
                    first = first.Next;
                    last = last.Previous;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        // 2. For a single linked list
        static bool IsPalindrome2(Node head)
        {
            if (head == null)
            {
                return false;
            }

            int k = 1;

            Node first = head;
            Node last = head;

            while (last.Next != null)
            {
                last = last.Next;
                k++;
            }

            bool result = false; ;

            bool done = false;

            while (!done)
            {
                if (first.Data == last.Data && first == last)
                {
                    result = true;
                    done = true;
                }
                else if (first.Data == last.Data)
                {
                    // Reset first and last
                    first = first.Next;
                    k = k - 2;

                    last = first;
                    for (int i = 0; i < k - 1; i++)
                    {
                        last = last.Next;
                    }
                }
                else
                {
                    result = false;
                    done = true;
                }
            }

            return result;
        }

        // Uses O(n) time and O(n) space
        static bool IsPalindrome3(Node head)
        {
            Stack<char> stack = new Stack<char>();

            Node current = head;

            while (current != null)
            {
                stack.Push(current.Data);
                current = current.Next;
            }

            current = head;


            while (current != null)
            {
                if (!current.Data.Equals(stack.Pop()))
                {
                    return false;
                }

                current = current.Next;
            }

            return true;
        }


        //This method takes O(n) time and O(1) extra space.
        //1) Get the middle of the linked list.
        //2) Reverse the second half of the linked list.
        //3) Check if the first half and second half are identical.
        //4) Construct the original linked list by reversing the second half again and attaching it back to the first half
        static bool IsPalindrome4(Node head)
        {
            Node current = head;
            Node runner = head;
            Node prev = null;

            Node mid = null;
            Node secondHalf = null;   

            while (runner != null && runner.Next != null)
            {
               prev = current;
               current = current.Next;
               runner = runner.Next.Next;
            }

            // if runner is not null
            // this means it the list 
            // has odd number of elements
            if (runner != null)
            {
               mid = current;
               secondHalf = mid.Next;
               prev.Next = null;
            }
            // This mean it has even
            // number of elements
            else
            {
               secondHalf = current;
               prev.Next = null;     
            }

            secondHalf = Reverse(secondHalf);

            bool result = Compare(head, secondHalf);

            // Restore the list
            secondHalf  = Reverse(secondHalf);

            if (mid != null)
            {  
               prev.Next = mid;
               mid.Next = secondHalf;
            }  
            else
            {
               prev.Next = secondHalf;
            }

            return result; 
        }

        // Example for reverse
        //prev  cu   next
        //a -   b -   c -  null

        //prev  cu    next
        //null - a    b -   c  - null

        //      prev   curr  next
        //null - a  -   b     c  - null

        //             prev  curr  next
        //null - a  -   b     c  - null

        //                    prev curr
        //null - a  -   b   -  c   null
        static Node Reverse(Node head)
        {
            Node prev = null;
            Node current = head;
            Node next = null;

            while (current != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }

            return prev;
        }

        static bool Compare(Node head1, Node head2)
        {
            Node current1 = head1;
            Node current2 = head2;

            while (current1 != null && current2 != null)
            {
                if (current1.Data.Equals(current2.Data))
                {
                    current1 = current1.Next;
                    current2 = current2.Next;
                }
                else
                {
                    break;
                }
            }

            if (current1 == null && current2 == null)
            {
                return true;
            }

            return false;
        }


        //left, right
        //   a        -  b  -    b -     a

        //left         right 
        //  a            b       b       a

        //left                    right 
        // a            b          b      a

        //left                            right
        // a            b          b       a 

                                                                                 
        //left                                  right
        // a            b          b       a     null


        //left                            right
        //  a           b          b       a

  
        //             left      right 
        // a             b          b       a
        // uses recursion
        static bool IsPalindrome5(Node head)
        {
            return IsPalindromeHelper(ref head, ref head);
        }

        static bool IsPalindromeHelper(ref Node left, ref Node right)
        {
            if (right == null)
            {
                return true;
            }

            bool result1 = IsPalindromeHelper(ref left, ref right.Next);

            if (!result1)
            {
                return result1;
            }

            bool result2 = left.Data.Equals(right.Data);

            left = left.Next;

            return result2;
        }


    }

    class Node
    {
        public Node(char data)
        {
            Data = data;
        }

        public char Data;
        public Node Next;
    }
}

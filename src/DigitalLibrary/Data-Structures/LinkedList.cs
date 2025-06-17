using System;
using DigitalLibrary.Books;

namespace DigitalLibrary.LinkedListManual
{
    public class Node
    {
        public Book val { get; set; }
        public Node next { get; set; }
        public Node prev { get; set; }

        public Node(Book val)
        {
            this.val = val;
        }
    }

    public class LinkedList
    {
        public Node head;
        public Node tail;

        public void Push(Book book)
        {
            Node newNode = new Node(book);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.next = newNode;
                newNode.prev = tail;
                tail = newNode;
            }
        }
        public bool Remove(string title)
        {
            Node curr = head;
            while (curr != null)
            {
                if (curr.val.Title.Trim().ToLower() == title.Trim().ToLower())
                {
                    if (curr.prev != null)
                    {
                        curr.prev.next = curr.next;
                    }
                    else
                    {
                        head = curr.next;
                    }

                    if (curr.next != null)
                    {
                        curr.next.prev = curr.prev;
                    }
                    else
                    {
                        tail = curr.prev;
                    }

                    return true; 
                }
                curr = curr.next;
            }
                return false;
        }

        public Book? Find(string title)
        {
            string searchTitle = title.Trim().ToLower();
            Node current = head;
            while (current != null)
            {
                if (current.val.Title.Trim().ToLower() == searchTitle)
                {
                    return current.val;
                }
                current = current.next;
            }
            return null;
        }
    }
}
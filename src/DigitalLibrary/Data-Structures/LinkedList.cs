using System;
using DigitalLibrary.Books;

namespace DigitalLibrary.LinkedListManual
{
    public class Node
    {
        public Book val { get; set; }
        public Node next { get; set; }

        public Node(Book val)
        {
            this.val = val;
            this.next = null!;
        }
    }

    public class LinkedList
    {
        private Node head;
        public LinkedList()
        {
            this.head = null!;
        }
        public void Push(Book book)
        {
            Node newNode = new Node(book);
            newNode.next = head;
            head = newNode;
        }
        public bool Remove(string title)
        {
            if (head == null) return false;
            if (head.val.Title == title)
            {
                head = head.next;
                return true;
            }
            Node curr = head;
            while (curr.next != null)
            {
                if (curr.next.val.Title == title)
                {
                    curr.next = curr.next.next;
                    return true;
                }
                curr = curr.next;
            }
            return false;
        }

        public Book? Find(string title)
        {
            Node current = head;
            while (current != null)
            {
                if (current.val.Title == title)
                {
                    return current.val;
                }
                current = current.next;
            }
            return null;
        }
    }
}
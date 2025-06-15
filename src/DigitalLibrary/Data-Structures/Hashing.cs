using System;
using DigitalLibrary.Books;
using DigitalLibrary.LinkedListManual;

namespace DigitalLibrary.HashTable
{
    public class BookHashTable
    {
        private int size = 100;
        private LinkedList[] buckets;

        public BookHashTable()
        {
            buckets = new LinkedList[size];
            for (int i = 0; i < size; i++)
            {
                buckets[i] = new LinkedList();
            }
        }

        private int GetIndex(string title)
        {
            int hash = Math.Abs(title.Trim().ToLower().GetHashCode());
            return hash % size;
        }

        public void Insert(string title, Book book)
        {
            int index = GetIndex(title);
            buckets[index].Push(book);
        }

        public Book? Get(string title)
        {
            int index = GetIndex(title);
            return buckets[index].Find(title);
        }

        public bool Remove(string title)
        {
            int index = GetIndex(title);
            return buckets[index].Remove(title);
        }
    }
}

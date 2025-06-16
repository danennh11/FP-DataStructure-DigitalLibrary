using System;
using DigitalLibrary.Books;
using DigitalLibrary.LinkedListManual;

namespace DigitalLibrary.HashTable
{
    public class BookHashTable
    {
        public int size = 100;
        public LinkedList[] buckets;

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
            string trimmedTitle = title.Trim().ToLower();
            string FirstTwoChars = trimmedTitle.Length >= 2 ? trimmedTitle.Substring(0, 2) : trimmedTitle;

            int hash = 0;
            foreach (char c in FirstTwoChars)
            {
                hash = (hash * 31 + c);
            }

            return Math.Abs(hash) % size;
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

using System;
using DigitalLibrary.Books;
using DigitalLibrary.LinkedListManual;

namespace DigitalLibrary.HashTable
{
    public class BookHashTable
    {
        public int size = 677;
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
            string trimmedTitle = title.Trim().ToLower().Replace(" ", "");
            char c1 = trimmedTitle.Length >= 1 && char.IsLetter(trimmedTitle[0]) ? trimmedTitle[0] : 'a';
            char c2 = trimmedTitle.Length >= 2 && char.IsLetter(trimmedTitle[1]) ? trimmedTitle[1] : 'a';

            int hash = ((c1 - 'a') * 26) + (c2 - 'a');

            return Math.Clamp(hash, 0, size - 1);
        }

        public void Insert(string title, Book book)
        {
            int index = GetIndex(title);
            Book exist = buckets[index].Find(title);
            
            if(exist == null)
            {
                buckets[index].Push(book);
            }
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

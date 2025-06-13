using System;
using DigitalLibrary.Books;
using DigitalLibrary.LinkedListManual;
using DigitalLibrary.HashTable;

namespace DigitalLibrary.Library
{
    public class DigLib
    {
        private BookHashTable TitleIndex;
        private LinkedList ListOfBooks;
        public DigLib()
        {
            TitleIndex = new BookHashTable();
            ListOfBooks = new LinkedList();
        }
        public void AddBook(string title, string author)
        {
            Book book = new Book(title, author);
            ListOfBooks.Push(book);
            TitleIndex.Insert(title, book);
        }
        public Book? SearchBook(string title)
        {
            return TitleIndex.Get(title);
        }
        public bool RemoveBook(string title)
        {
            bool removedFromHash = TitleIndex.Remove(title);
            bool removedFromList = ListOfBooks.Remove(title);
            return removedFromHash && removedFromList;
        }
    }
}
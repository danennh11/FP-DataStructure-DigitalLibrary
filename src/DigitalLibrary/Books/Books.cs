using System;

namespace DigitalLibrary.Books
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public Book(string title, string author)
        {
            this.Title = title;
            Author = author;
        }
        public void Display()
        {
            System.Console.WriteLine($"Title: {Title}, Author: {Author}");
        }
    }
}
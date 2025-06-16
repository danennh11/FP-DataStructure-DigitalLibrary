using System;
using DigitalLibrary.HashTable;
using DigitalLibrary.Library;
using DigitalLibrary.LinkedListManual;
using DigitalLibrary.Books;

namespace DigitalLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var library = new DigLib();

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
            if (File.Exists(filePath))
            {
                Console.WriteLine("test");
                string[] list = File.ReadAllLines(filePath);
                foreach (string titles in list)
                {
                    string[] parts = titles.Split(',');
                    if (parts.Length != 2) continue;
                    string title = parts[0];
                    string author = parts[1];

                    library.AddBook(title, author);
                }
            }

            CommandLine(library);
        }

        public static void CommandLine(DigLib library)
        {
            while (true)
            {
                System.Console.WriteLine("Welcome to the Digital Library!\n" +
                    "A. Add Book\n" +
                    "S. Search Book\n" +
                    "R. Remove Book\n" +
                    "L. Show All Book\n" +
                    "E. Exit\n" +
                    "Please select an option:");
                var CMD = Console.ReadLine()?.ToUpper();
                if (CMD == null)
                {
                    continue;
                }
                switch (CMD)
                {
                    case "A":
                        Console.Write("Enter book title: ");
                        string title = Console.ReadLine() ?? string.Empty;
                        Console.Write("Enter book author: ");
                        string author = Console.ReadLine() ?? string.Empty;
                        library.AddBook(title, author);
                        Console.WriteLine($"Book '{title}' by {author} added successfully.");

                        Console.WriteLine();
                        break;

                    case "S":
                        Console.Write("Enter book title to search: ");
                        string searchTitle = Console.ReadLine() ?? string.Empty;
                        var book = library.SearchBook(searchTitle);
                        if (book != null)
                        {
                            Console.WriteLine($"Found book: {book.Title} by {book.Author}");
                        }
                        else
                        {
                            Console.WriteLine($"Book '{searchTitle}' not found.");
                        }

                        Console.WriteLine();
                        break;

                    case "R":
                        Console.Write("Enter book title to remove: ");
                        string removeTitle = Console.ReadLine() ?? string.Empty;
                        bool removed = library.RemoveBook(removeTitle);
                        if (removed)
                        {
                            Console.WriteLine($"Book '{removeTitle}' removed successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Book '{removeTitle}' not found or could not be removed.");
                        }

                        Console.WriteLine();
                        break;
                    case "L":
                        ShowAllBooks(library);
                        break;
                    case "E":
                        return;

                    default:
                        Console.WriteLine("Invalid option.\n" +
                        "Please select an option:");
                        break;
                }
            }
        }

        public static void ShowAllBooks(DigLib library)
        {
            var table = library.TitleIndex.buckets;
            int size = library.TitleIndex.size;
            LinkedList listOfBooks = new LinkedList();

            for (int i = 0; i < size; i++)
            {
                LinkedList bucket = table[i];

                Node ptr = bucket.head;
                while (ptr != null)
                {
                    listOfBooks.Push(ptr.val);
                    ptr = ptr.next;
                }
            }

            Node current = listOfBooks.head;
            int page = 1;
            int bookPerPage = 100;
            int count = 0;

            while (current != null)
            {
                Console.WriteLine($"{count + 1}. {current.val.Title} by {current.val.Author}");
                count++;

                if (count % bookPerPage == 0)
                {
                    Console.WriteLine($"\n -- Page {page} --\n -- Press Enter to Continue --\n -- Press E to Exit --");
                    string cmd = Console.ReadLine();
                    
                    if (cmd == null)
                    {
                        page++;
                    }
                    else if (cmd.ToLower() == "e")
                    {
                        break;
                    }

                    page++;
                }

                current = current.next;
            }

            Console.WriteLine();
        }
    }
}

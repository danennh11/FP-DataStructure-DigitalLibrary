using System;
using DigitalLibrary.HashTable;
using DigitalLibrary.Library;
using DigitalLibrary.LinkedListManual;
using DigitalLibrary.Books;

namespace DigitalLibrary
{
    public class Program
    {
        static int dataCount = 0;

        public static void Main(string[] args)
        {
            var library = new DigLib();

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.csv");

            if (File.Exists(filePath))
            {
                string[] list = File.ReadAllLines(filePath);
                string[] header = list[0].Split(',');

                int indexTitle = Array.IndexOf(header, "bookTitle");
                int indexAuthor = Array.IndexOf(header, "authorName");

                for (int i = 1; i < list.Length; i++)
                {
                    string[] data = list[i].Split(',');

                    string book = data[indexTitle].Replace("\"", "");
                    string author = data[indexAuthor];

                    if (!string.IsNullOrWhiteSpace(book) && !string.IsNullOrWhiteSpace(author))
                    {
                        library.AddBook(book, author);
                        dataCount++;
                    }
                }

                Console.WriteLine($"✅ Loaded {dataCount} books successfully!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            CommandLine(library);
        }

        public static void CommandLine(DigLib library)
        {
            while (true)
            {
                Console.Clear();
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
                        dataCount++;
                        Console.WriteLine($"Book '{title}' by {author} added successfully.");

                        Console.ReadLine();
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

                        Console.ReadLine();
                        Console.WriteLine();
                        break;

                    case "R":
                        Console.Write("Enter book title to remove: ");
                        string removeTitle = Console.ReadLine() ?? string.Empty;
                        bool removed = library.RemoveBook(removeTitle);
                        if (removed)
                        {
                            Console.WriteLine($"Book '{removeTitle}' removed successfully.");
                            dataCount--;
                        }
                        else
                        {
                            Console.WriteLine($"Book '{removeTitle}' not found or could not be removed.");
                        }

                        Console.WriteLine();
                        break;
                    case "L":
                        Console.Clear();
                        ShowAllBooks(library);
                        break;
                    case "E":
                        Console.WriteLine("\n👋 Thank you for using Digital Library!");
                        Console.ReadLine();
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

            Node currentPage = listOfBooks.head;
            int page = 1;
            int bookPerPage = 100;

            while (currentPage != null)
            {
                Console.Clear();
                Node temp = currentPage;

                for (int i = 0; i < bookPerPage && temp != null; i++)
                {
                    Console.WriteLine($"{(page - 1) * bookPerPage + i + 1}. {temp.val.Title} by {temp.val.Author}");
                    temp = temp.next;
                }

                Console.WriteLine(new string('=', 50));
                Console.WriteLine($" Page {page} of {Math.Max(1, (int)Math.Ceiling((double)dataCount / bookPerPage))} ");
                Console.WriteLine(new string('=', 50));

                Console.WriteLine(" < Prev | Next > | E Exit");
                string input = Console.ReadLine()?.ToLower();

                if (input == "e") break;
                else if (input == "<" || input == ",")
                {
                    for (int i = 0; i < bookPerPage && currentPage.prev != null; i++)
                    {
                        currentPage = currentPage.prev;
                    }
                    page = Math.Max(1, page - 1);
                }
                else if (input == ">" || input == ".")
                {
                    for (int i = 0; i < bookPerPage && currentPage.next != null; i++)
                    {
                        currentPage = currentPage.next;
                    }
                    page++;
                }
            }

            Console.WriteLine();
        }
    }
}

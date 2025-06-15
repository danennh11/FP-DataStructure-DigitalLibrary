using System;
using DigitalLibrary.Library;

namespace DigitalLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var library = new DigLib();

            string filePath  = "data.txt";

            try
            {
                if (File.Exists(filePath))
                {
                    string[] list = File.ReadAllLines(filePath);
                    foreach (string titles in list)
                    {
                        string[] parts = titles.Split(',');
                        string title = parts[0];
                        string author = parts[1];

                        library.AddBook(title, author);
                    }
                }
            } catch
            {
                Console.WriteLine("Cannot find the file, please provide the correct file path!");
            }

            while (true)
            {
                System.Console.WriteLine("Welcome to the Digital Library!\n" +
                    "A. Add Book\n" +
                    "S. Search Book\n" +
                    "R. Remove Book\n" +
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

                    case "E":
                        return;

                    default:
                        Console.WriteLine("Invalid option.\n" +                
                        "Please select an option:");
                        break;
                }
            }

        }
    }
}

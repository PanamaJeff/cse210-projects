using System;
using System.Collections.Generic;

public class Book
{
    public string Title { get; private set; }
    public string Author { get; private set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    // TODO: Add methods and additional properties for the Book class
}

public class BookCategory
{
    // TODO: Add properties and methods for the BookCategory class
}

public abstract class Person
{
    public string Name { get; protected set; }
    public int Id { get; protected set; }

    // TODO: Add methods and additional properties for the Person class
}

public class Employee : Person
{
    // TODO: Add properties and methods for the Employee class
}

public class Librarian : Employee
{
    // TODO: Add properties and methods for the Librarian class
}

public class Member : Person
{
    // TODO: Add properties and methods for the Member class
}

public class Library
{
    private List<Book> books;

    public Library()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void DisplayBooks()
    {
        foreach (var book in books)
        {
            Console.WriteLine($"Title: {book.Title}\tAuthor: {book.Author}");
        }
    }

    // TODO: Add additional methods and properties for the Library class
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create a new library
        Library library = new Library();

        // Add some books
        library.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald"));
        library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee"));

        // Display the books
        library.DisplayBooks();

        // TODO: Add further functionality and interactions in the Main method
    }
}
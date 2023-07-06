using System;
using System.Collections.Generic;

public class Book
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public bool IsAvailable { get; private set; }
    public BookCategory Category { get; private set; }

    public Book(string title, string author, string isbn, BookCategory category)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsAvailable = true;
        Category = category;
    }

    public void Borrow()
    {
        if (IsAvailable)
        {
            IsAvailable = false;
        }
        else
        {
            throw new Exception("Book is not available.");
        }
    }

    public void Return()
    {
        IsAvailable = true;
    }
}

public class BookCategory
{
    public string CategoryName { get; private set; }

    public BookCategory(string categoryName)
    {
        CategoryName = categoryName;
    }
}

public abstract class Person
{
    public string Name { get; protected set; }
    public int Id { get; protected set; }

    public virtual void DisplayRole()
    {
        Console.WriteLine("This is a person.");
    }
}

public class Employee : Person
{
    public string Position { get; private set; }

    public Employee(string name, int id, string position)
    {
        Name = name;
        Id = id;
        Position = position;
    }

    public override void DisplayRole()
    {
        Console.WriteLine($"{Name} is an employee with the position of {Position}.");
    }
}

public class Librarian : Employee
{
    public Librarian(string name, int id, string position) : base(name, id, position)
    {
    }

    public void OrganizeBooks(Library library)
    {
        Console.WriteLine($"{Name} is organizing books in the library.");
        // Logic for organizing books...
    }
}

public class Member : Person
{
    public List<Book> BooksBorrowed { get; private set; }

    public Member(string name, int id)
    {
        Name = name;
        Id = id;
        BooksBorrowed = new List<Book>();
    }

    public override void DisplayRole()
    {
        Console.WriteLine($"{Name} is a library member.");
    }

    public void BorrowBook(Book book)
    {
        book.Borrow();
        BooksBorrowed.Add(book);
    }

    public void ReturnBook(Book book)
    {
        book.Return();
        BooksBorrowed.Remove(book);
    }
}

public class Library
{
    private List<Book> books;
    private List<Person> people;

    public Library()
    {
        books = new List<Book>();
        people = new List<Person>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void AddPerson(Person person)
    {
        people.Add(person);
    }

    public Book FindBookByTitle(string title)
    {
        foreach (var book in books)
        {
            if (book.Title == title)
            {
                return book;
            }
        }

        return null;
    }

    public Person FindPersonByName(string name)
    {
        foreach (var person in people)
        {
            if (person.Name == name)
            {
                return person;
            }
        }

        return null;
    }

    public IEnumerable<Person> GetPeople()
    {
        foreach (var person in people)
        {
            yield return person;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create a new library
        Library library = new Library();

        // Create book categories
        BookCategory fiction = new BookCategory("Fiction");

        // Add some books
        library.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", "9780743273565", fiction));
        library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "9780446310789", fiction));

        // Add some people
        library.AddPerson(new Librarian("John Doe", 1, "Librarian"));
        library.AddPerson(new Member("Jane Smith", 2));

        // Find a book and a member
        Book book = library.FindBookByTitle("The Great Gatsby");
        Member member = library.FindPersonByName("Jane Smith") as Member;

        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        if (member == null)
        {
            Console.WriteLine("Member not found.");
            return;
        }

        // Have the member borrow the book
        member.BorrowBook(book);

        Console.WriteLine($"{member.Name} has borrowed {book.Title}.");

        // Display roles
        foreach (Person person in library.GetPeople())
        {
            person.DisplayRole();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

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

        while (true)
        {
            Console.WriteLine("1. Borrow a book");
            Console.WriteLine("2. Return a book");
            Console.WriteLine("3. Add a book");
            Console.WriteLine("4. Add a person");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the name of the book: ");
                    string bookTitle = Console.ReadLine();

                    Console.Write("Enter the name of the member: ");
                    string memberName = Console.ReadLine();

                    Book book = library.FindBookByTitle(bookTitle);
                    Member member = library.FindPersonByName(memberName) as Member;

                    if (book == null)
                    {
                        Console.WriteLine("Book not found.");
                        continue;
                    }

                    if (member == null)
                    {
                        Console.WriteLine("Member not found.");
                        continue;
                    }

                    // Have the member borrow the book
                    member.BorrowBook(book);

                    Console.WriteLine($"{member.Name} has borrowed {book.Title}.");
                    break;

                case "2":
                    Console.Write("Enter the name of the book: ");
                    bookTitle = Console.ReadLine();

                    Console.Write("Enter the name of the member: ");
                    memberName = Console.ReadLine();

                    book = library.FindBookByTitle(bookTitle);
                    member = library.FindPersonByName(memberName) as Member;

                    if (book == null)
                    {
                        Console.WriteLine("Book not found.");
                        continue;
                    }

                    if (member == null)
                    {
                        Console.WriteLine("Member not found.");
                        continue;
                    }

                    // Have the member return the book
                    member.ReturnBook(book);

                    Console.WriteLine($"{member.Name} has returned {book.Title}.");
                    break;

                case "3":
                    Console.Write("Enter the title of the book: ");
                    string title = Console.ReadLine();

                    Console.Write("Enter the author of the book: ");
                    string author = Console.ReadLine();

                    Console.Write("Enter the ISBN of the book: ");
                    string isbn = Console.ReadLine();
                     // Here we're assuming all new books are fiction, you could add more options to handle other categories
                    library.AddBook(new Book(title, author, isbn, fiction));
                    Console.WriteLine("Book added successfully.");
                    break;

                case "4":
                    Console.Write("Enter the name of the person: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter the role of the person (1- Librarian, 2- Member): ");
                    string role = Console.ReadLine();

                    if (role == "1")
                    {
                        Console.Write("Enter the position of the Librarian: ");
                        string position = Console.ReadLine();
                        library.AddPerson(new Librarian(name, library.GetPeople().Count() + 1, position));
                    }
                    else if (role == "2")
                    {
                        library.AddPerson(new Member(name, library.GetPeople().Count() + 1));
                    }
                    else
                    {
                        Console.WriteLine("Invalid role selected.");
                        continue;
                    }

                    Console.WriteLine("Person added successfully.");
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
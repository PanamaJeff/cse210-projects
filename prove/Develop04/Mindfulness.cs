using System;
using System.Threading;

public abstract class Activity
{
    protected string Name;
    protected string Description;
    protected int Duration;

    public Activity(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Starting {Name} activity. {Description}");
        Console.Write("Set the duration of the activity in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000); // Pause for 3 seconds
    }

    public abstract void RunActivity();

    public void EndActivity()
    {
        Console.WriteLine("You have done a good job!");
        Thread.Sleep(3000); // Pause for 3 seconds
        Console.WriteLine($"You have completed {Name} activity for {Duration} seconds.");
        Thread.Sleep(3000); // Pause for 3 seconds
    }
}

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void RunActivity()
    {
        var startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < Duration)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(3000); // Pause for 3 seconds
            Console.WriteLine("Breathe out...");
            Thread.Sleep(3000); // Pause for 3 seconds
        }
    }
}

public class ReflectionActivity : Activity
{
    private string[] prompts = new string[]
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        // Add more prompts here...
    };

    private string[] questions = new string[]
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        // Add more questions here...
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    public override void RunActivity()
    {
        var random = new Random();
        var prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);

        var startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < Duration)
        {
            var question = questions[random.Next(questions.Length)];
            Console.WriteLine(question);
            Thread.Sleep(3000); // Pause for 3 seconds
        }
    }
}

public class ListingActivity : Activity
{
    private string[] prompts = new string[]
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        // Add more prompts here...
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void RunActivity()
    {
        var random = new Random();
        var prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);

        var startTime = DateTime.Now;
        int itemCount = 0;
        while ((DateTime.Now - startTime).TotalSeconds < Duration)
        {
            Console.Write("List an item: ");
            var item =Console.ReadLine();
            itemCount++;
            Thread.Sleep(3000); // Pause for 3 seconds
        }

        Console.WriteLine($"You have listed {itemCount} items.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Exit");
            Console.Write("Your choice: ");
            var choice = Console.ReadLine();

            Activity activity = null;
            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
            }

            activity.StartActivity();
            activity.RunActivity();
            activity.EndActivity();
        }
    }
}
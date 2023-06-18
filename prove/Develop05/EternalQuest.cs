using System;
using System.Collections.Generic;

// Base class for all goals
public abstract class Goal
{
    public string Name { get; set; }
    public int Value { get; set; }
    public bool Completed { get; protected set; }

    public abstract void RecordEvent();
    public abstract string GetProgress();
}

// Simple goal that can be marked complete
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int value)
    {
        Name = name;
        Value = value;
        Completed = false;
    }

    public override void RecordEvent()
    {
        Completed = true;
    }

    public override string GetProgress()
    {
        return Completed ? "Completed" : "Not Completed";
    }
}

// Eternal goal that gives points each time it is recorded
public class EternalGoal : Goal
{
    public EternalGoal(string name, int value)
    {
        Name = name;
        Value = value;
        Completed = false;
    }

    public override void RecordEvent()
    {
        Completed = true;
    }

    public override string GetProgress()
    {
        return Completed ? "Recorded" : "Not Recorded";
    }
}

// Checklist goal that must be accomplished a certain number of times
public class ChecklistGoal : Goal
{
    private int timesCompleted;
    private int requiredTimes;

    public ChecklistGoal(string name, int value, int requiredTimes)
    {
        Name = name;
        Value = value;
        Completed = false;
        this.requiredTimes = requiredTimes;
    }

    public override void RecordEvent()
    {
        timesCompleted++;
        if (timesCompleted >= requiredTimes)
            Completed = true;
    }

    public override string GetProgress()
    {
        return $"Completed {timesCompleted}/{requiredTimes} times";
    }
}

// Program class to handle user interaction
public class Program
{
    private List<Goal> goals;
    private int score;

    public Program()
    {
        goals = new List<Goal>();
        score = 0;
    }

    public void Run()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Record an event");
            Console.WriteLine("3. Display goals and progress");
            Console.WriteLine("4. Display score");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    RecordEvent();
                    break;
                case "3":
                    DisplayGoals();
                    break;
                case "4":
                    DisplayScore();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private void CreateGoal()
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal type (1 - Simple, 2 - Eternal, 3 - Checklist): ");
        string typeChoice = Console.ReadLine();

        int value;
        while (true)
        {
            Console.Write("Enter goal value: ");
            if (int.TryParse(Console.ReadLine(), out value))
                break;
            else
                Console.WriteLine("Invalid value. Please enter a valid integer.");
        }

        switch (typeChoice)
        {
            case "1":
                goals.Add(new SimpleGoal(name, value));
                break;
            case "2":
                goals.Add(new EternalGoal(name, value));
                 break;
            case "3":
                int requiredTimes;
                while (true)
                {
                    Console.Write("Enter the required number of completions: ");
                    if (int.TryParse(Console.ReadLine(), out requiredTimes) && requiredTimes > 0)
                        break;
                    else
                        Console.WriteLine("Invalid value. Please enter a positive integer.");
                }
                goals.Add(new ChecklistGoal(name, value, requiredTimes));
                break;
            default:
                Console.WriteLine("Invalid goal type. Goal not created.");
                break;
        }

        Console.WriteLine("Goal created successfully.");
    }

    private void RecordEvent()
    {
        Console.Write("Enter the index of the goal to record an event for: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < goals.Count)
        {
            Goal goal = goals[index];
            goal.RecordEvent();
            score += goal.Value;
            Console.WriteLine("Event recorded successfully.");
        }
        else
        {
            Console.WriteLine("Invalid index. Please enter a valid goal index.");
        }
    }

    private void DisplayGoals()
    {
        Console.WriteLine("Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Goal goal = goals[i];
            Console.WriteLine($"[{i}] {goal.Name} - {goal.GetProgress()}");
        }
    }

    private void DisplayScore()
    {
        Console.WriteLine($"Score: {score}");
    }
}

// Main entry point of the program
public class MainEntry
{
    public static void Main(string[] args)
    {
        Program program = new Program();
        program.Run();
    }
}
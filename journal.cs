using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JournalProgram
{
    public class Entry
    {
        public string Date { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }

        public Entry(string date, string prompt, string response)
        {
            Date = date;
            Prompt = prompt;
            Response = response;
        }

        public override string ToString()
        {
            return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
        }
    }

    public class Journal
    {
        private List<Entry> entries = new List<Entry>();

        public void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }

        public void DisplayEntries()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void SaveToFile(string filename)
        {
            using StreamWriter file = new StreamWriter(filename);
            foreach (var entry in entries)
            {
                file.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }

        public void LoadFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                entries.Clear();
                using StreamReader file = new StreamReader(filename);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        entries.Add(new Entry(parts[0], parts[1], parts[2]));
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found!");
            }
        }
    }

    class Program
    {
        private static string[] prompts = new string[]
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        static void Main(string[] args)
        {
            Journal journal = new Journal();
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    switch (option)
                    {
                        case 1:
                            WriteNewEntry(journal);
                            break;
                        case 2:
                            journal.DisplayEntries();
                            break;
                        case 3:
                            SaveJournal(journal);
                            break;
                        case 4:
                            LoadJournal(journal);
                            break;
                        case 5:
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
            }
        }

        private static void WriteNewEntry(Journal journal)
        {
            Random random = new Random();
            int index = random.Next(prompts.Length);
            string prompt = prompts[index];

            Console.WriteLine($"Prompt: {prompt}");
            Console.Write("Your response: ");
            string response = Console.ReadLine();

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            journal.AddEntry(new Entry(date, prompt, response));
        }

        private static void SaveJournal(Journal journal)
        {
            Console.Write("Enter filename to save: ");
            string filename = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(filename))
            {
                journal.SaveToFile(filename);
                Console.WriteLine("Journal saved successfully!");
            }
            else
            {
                Console.WriteLine("Invalid filename!");
            }
        }

        private static void LoadJournal(Journal journal)
        {
            Console.Write("Enter filename to load: ");
            string filename = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(filename))
            {
                journal.LoadFromFile(filename);
                Console.WriteLine("Journal loaded successfully!");
            }
            else
            {
                Console.WriteLine("Invalid filename!");
            }
        }
    }
}
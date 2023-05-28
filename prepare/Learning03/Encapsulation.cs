using System;
using System.Collections.Generic;
using System.Linq;

public class ScriptureReference
{
    public string Book { get; set; }
    public int Chapter { get; set; }
    public int Verse { get; set; }
    public int? EndVerse { get; set; }

    public ScriptureReference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        Verse = verse;
    }

    public ScriptureReference(string book, int chapter, int verse, int endVerse)
        : this(book, chapter, verse)
    {
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return $"{Book} {Chapter}:{Verse}" + (EndVerse.HasValue ? $"-{EndVerse}" : string.Empty);
    }
}

public class Word
{
    public string Value { get; set; }
    public bool IsHidden { get; set; }

    public Word(string value)
    {
        Value = value;
        IsHidden = false;
    }
}

public class Scripture
{
    public ScriptureReference Reference { get; set; }
    public List<Word> Words { get; set; }

    public Scripture(string book, int chapter, int verse, string text)
    {
        Reference = new ScriptureReference(book, chapter, verse);
        Words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public Scripture(string book, int chapter, int verse, int endVerse, string text)
        : this(book, chapter, verse, text)
    {
        Reference.EndVerse = endVerse;
    }

    public Scripture(string line)
    {
        var parts = line.Split(',');
        if (parts.Length >= 4 && !string.IsNullOrEmpty(parts[3]))
        {
            Reference = new ScriptureReference(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
        }
        else
        {
            Reference = new ScriptureReference(parts[0], int.Parse(parts[1]), int.Parse(parts[2]));
        }
        Words = parts.Last().Split(' ').Select(w => new Word(w)).ToList();
    }

    public override string ToString()
    {
        string scriptureText = string.Join(" ", Words.Select(w => w.IsHidden ? "_____" : w.Value));
        return $"{Reference.ToString()}\n{scriptureText}";
    }

    public void HideRandomWord()
    {
        var visibleWords = Words.Where(w => !w.IsHidden).ToList();
        if (visibleWords.Any())
        {
            var random = new Random();
            int index = random.Next(visibleWords.Count);
            visibleWords[index].IsHidden = true;
        }
    }

    public bool AllWordsHidden => Words.All(w => w.IsHidden);
}

public class ScriptureLibrary
{
    public List<Scripture> Scriptures { get; set; }

    public ScriptureLibrary(string filename)
    {
        Scriptures = new List<Scripture>();
        LoadScripturesFromFile(filename);
    }

    private void LoadScripturesFromFile(string filename)
    {
        var lines = System.IO.File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            Scriptures.Add(new Scripture(line));
        }
    }

    public Scripture GetRandomScripture()
    {
        var random = new Random();
        int index = random.Next(Scriptures.Count);
        return Scriptures[index];
    }
}

class Program
{
    static void Main(string[] args)
    {
        var scriptureLibrary = new ScriptureLibrary("scriptures.txt");
        var scripture = scriptureLibrary.GetRandomScripture();

        while (true)
        {
            Console.Clear();
                Console.WriteLine(scripture.ToString());
            if (scripture.AllWordsHidden)
            {
                break;
            }

            Console.Write("\nPress Enter to hide a word or type 'quit' to quit: ");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWord();
        }
    }
}
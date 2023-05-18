using System;
using System.Collections.Generic;

public class Word
{
    public string Text { get; set; }
    public bool IsHidden { get; set; }
}

public class ScriptureReference
{
    public string Book { get; set; }
    public int Chapter { get; set; }
    public int VerseStart { get; set; }
    public int VerseEnd { get; set; }

    public ScriptureReference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verse;
        VerseEnd = verse;
    }

    public ScriptureReference(string book, int chapter, int verseStart, int verseEnd)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verseStart;
        VerseEnd = verseEnd;
    }

    public override string ToString()
    {
        if (VerseStart == VerseEnd)
        {
            return $"{Book} {Chapter}:{VerseStart}";
        }
        else
        {
            return $"{Book} {Chapter}:{VerseStart}-{VerseEnd}";
        }
    }
}

public class Scripture
{
    public ScriptureReference Reference { get; set; }
    public List<Word> Words { get; set; }

    public Scripture(ScriptureReference reference, string text)
    {
        Reference = reference;
        Words = new List<Word>();
        string[] words = text.Split(' ');
        foreach (var word in words)
        {
            Words.Add(new Word { Text = word, IsHidden = false });
        }
    }

    public void HideRandomWord()
    {
        Random random = new Random();
        int index = random.Next(Words.Count);
        Words[index].IsHidden = true;
    }

    public override string ToString()
    {
        string scriptureText = "";
        foreach (var word in Words)
        {
            if (word.IsHidden)
            {
                scriptureText += "____ ";
            }
            else
            {
                scriptureText += word.Text + " ";
            }
        }

        return $"{Reference.ToString()}\n{scriptureText}";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        ScriptureReference reference = new ScriptureReference("John", 3, 16);
        Scripture scripture = new Scripture(reference, "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.ToString());

            Console.Write("\nPress enter to hide a word or type 'quit' to exit: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWord();

            if (scripture.Words.TrueForAll(word => word.IsHidden))
            {
                break;
            }
        }

        Console.Clear();
        Console.WriteLine("Thank you for using the program. Goodbye!");
    }
}
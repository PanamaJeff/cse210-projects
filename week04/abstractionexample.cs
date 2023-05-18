using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Entry
{
    public DateTime Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
}

public abstract class AbstractJournal
{
    private List<Entry> entries;

    public AbstractJournal()
    {
        entries = new List<Entry>();
    }

    public void AddEntry(Entry entry)
    {
        entries.Add(entry);
    }

    public bool RemoveEntry(Entry entry)
    {
        return entries.Remove(entry);
    }

    // You might want to add an abstract method here, depending on your design
    public abstract void DisplayEntries();
}
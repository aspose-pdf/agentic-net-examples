using System;
using System.IO;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string inputPath = "source.xml";
        const string outputPath = "output.txt";

        // Verify the source XML exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the XML document
        XDocument doc = XDocument.Load(inputPath);

        // Default values to use when elements are missing or invalid
        string defaultTitle = "Untitled Document";
        string defaultAuthor = "Unknown Author";
        DateTime defaultDate = DateTime.Today;
        int defaultPageCount = 1;

        // Retrieve values with fallback to defaults
        string title = (string)doc.Root.Element("Title") ?? defaultTitle;
        string author = (string)doc.Root.Element("Author") ?? defaultAuthor;
        DateTime date = ParseDate((string)doc.Root.Element("Date")) ?? defaultDate;
        int pageCount = ParseInt((string)doc.Root.Element("PageCount")) ?? defaultPageCount;

        // Write the resolved values to an output file
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            writer.WriteLine($"Title: {title}");
            writer.WriteLine($"Author: {author}");
            writer.WriteLine($"Date: {date:yyyy-MM-dd}");
            writer.WriteLine($"PageCount: {pageCount}");
        }

        Console.WriteLine($"Processed XML and wrote results to '{outputPath}'.");
    }

    // Safely parse a date string; returns null if parsing fails
    static DateTime? ParseDate(string value)
    {
        return DateTime.TryParse(value, out DateTime result) ? result : (DateTime?)null;
    }

    // Safely parse an integer string; returns null if parsing fails
    static int? ParseInt(string value)
    {
        return int.TryParse(value, out int result) ? result : (int?)null;
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Proper nouns with desired casing
        var properNouns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "nasa", "NASA" },
            { "asp.net", "ASP.NET" },
            { "pdf", "PDF" },
            { "aspose", "Aspose" }
        };

        // Load the PDF using the core Document API (not PdfBookmarkEditor)
        Document doc = new Document(inputPath);

        // Update bookmark (outline) titles recursively
        UpdateOutlineTitles(doc.Outlines, properNouns);

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"Bookmarks updated and saved to '{outputPath}'.");
    }

    // Recursively walk the outline collection and fix titles
    static void UpdateOutlineTitles(IEnumerable<OutlineItemCollection> items, Dictionary<string, string> properNouns)
    {
        foreach (OutlineItemCollection item in items)
        {
            item.Title = ToTitleCase(item.Title, properNouns);
            // The item itself is a collection of its children, so we can recurse directly.
            if (item.Count > 0)
                UpdateOutlineTitles(item, properNouns);
        }
    }

    static string ToTitleCase(string text, Dictionary<string, string> properNouns)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        var words = text.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            if (properNouns.TryGetValue(word, out string proper))
            {
                words[i] = proper;
            }
            else
            {
                if (word.Length > 1)
                    words[i] = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                else
                    words[i] = word.ToUpper();
            }
        }
        return string.Join(" ", words);
    }
}

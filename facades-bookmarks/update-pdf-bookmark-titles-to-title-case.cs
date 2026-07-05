using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Converts a string to title case while preserving proper nouns.
    static string ToTitleCase(string text, IDictionary<string, string> properNouns)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        TextInfo ti = CultureInfo.InvariantCulture.TextInfo;
        string[] words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < words.Length; i++)
        {
            string lower = words[i].ToLowerInvariant();
            if (properNouns.TryGetValue(lower, out string proper))
            {
                words[i] = proper; // keep the proper noun as defined
            }
            else
            {
                words[i] = ti.ToTitleCase(lower);
            }
        }
        return string.Join(" ", words);
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Define proper nouns (key = lower‑case, value = desired case)
        var properNouns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "aspose", "Aspose" },
            { "pdf", "PDF" },
            { "csharp", "CSharp" },
            { "dotnet", ".NET" },
            { "nasa", "NASA" }
        };

        // Initialize the bookmark editor and bind the PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Extract all bookmarks (including nested ones)
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Iterate through each bookmark and update its title
        foreach (Bookmark bm in allBookmarks)
        {
            string originalTitle = bm.Title;
            string newTitle = ToTitleCase(originalTitle, properNouns);
            if (!originalTitle.Equals(newTitle, StringComparison.Ordinal))
            {
                editor.ModifyBookmarks(originalTitle, newTitle);
            }
        }

        // Save the modified PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Bookmarks updated and saved to '{outputPdf}'.");
    }
}
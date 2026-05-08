using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    // Predefined list of proper nouns (case‑insensitive match)
    static readonly HashSet<string> ProperNouns = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "NASA",
        "Aspose",
        "PDF"
        // add more proper nouns as needed
    };

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor to work with bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF
            editor.BindPdf(inputPdf);

            // Extract all bookmarks (recursive)
            Bookmarks bookmarks = editor.ExtractBookmarks();

            // Process each bookmark
            foreach (Bookmark bm in bookmarks)
            {
                string oldTitle = bm.Title;
                if (string.IsNullOrEmpty(oldTitle))
                    continue;

                string newTitle = ConvertToTitleCase(oldTitle);
                if (!oldTitle.Equals(newTitle, StringComparison.Ordinal))
                {
                    // Modify the bookmark title
                    editor.ModifyBookmarks(oldTitle, newTitle);
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks updated and saved to '{outputPdf}'.");
    }

    // Converts a string to title case, preserving predefined proper nouns
    static string ConvertToTitleCase(string text)
    {
        // Split on whitespace; keep delimiters (e.g., hyphens) simple
        var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var transformed = words.Select(word =>
        {
            // If the word matches a proper noun, use the exact casing from the list
            if (ProperNouns.Contains(word))
                return ProperNouns.First(p => p.Equals(word, StringComparison.OrdinalIgnoreCase));

            // Otherwise, title‑case the word (first letter upper, rest lower)
            if (word.Length == 1)
                return char.ToUpperInvariant(word[0]).ToString();

            return char.ToUpperInvariant(word[0]) + word.Substring(1).ToLowerInvariant();
        });

        return string.Join(" ", transformed);
    }
}
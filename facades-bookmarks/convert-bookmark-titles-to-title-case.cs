using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Predefined list of proper nouns (case‑insensitive lookup) with their correct casing
    private static readonly Dictionary<string, string> ProperNouns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "NASA", "NASA" },
        { "ASP.NET", "ASP.NET" },
        { "CSharp", "CSharp" },
        { "Microsoft", "Microsoft" },
        { "PDF", "PDF" }
    };

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF file to the bookmark editor (Facades API)
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract existing bookmarks, convert each title to title case while preserving proper nouns,
        // and apply the change via ModifyBookmarks.
        Bookmarks bookmarks = editor.ExtractBookmarks();
        foreach (Bookmark bm in bookmarks)
        {
            string originalTitle = bm.Title ?? string.Empty;
            string newTitle = ConvertToTitleCase(originalTitle);
            if (!string.Equals(originalTitle, newTitle, StringComparison.Ordinal))
            {
                // ModifyBookmarks updates all bookmarks that match the old title.
                editor.ModifyBookmarks(originalTitle, newTitle);
            }
        }

        // Save the modified PDF and release resources.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks updated and saved to '{outputPath}'.");
    }

    // Convert a string to title case, preserving predefined proper nouns
    private static string ConvertToTitleCase(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        string[] words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];

            // Preserve proper noun if it exists in the list (case‑insensitive lookup)
            if (ProperNouns.TryGetValue(word, out string proper))
            {
                words[i] = proper;
                continue;
            }

            // Simple title‑case conversion: first char upper, rest lower
            if (word.Length > 1)
                words[i] = char.ToUpperInvariant(word[0]) + word.Substring(1).ToLowerInvariant();
            else
                words[i] = word.ToUpperInvariant();
        }

        return string.Join(" ", words);
    }
}

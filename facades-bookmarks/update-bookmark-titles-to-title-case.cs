using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    // Converts a title to title case while preserving predefined proper nouns.
    static string ToTitleCase(string input, HashSet<string> properNouns)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        // Split on spaces; keep delimiters simple for this example.
        var words = input.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            // Preserve proper noun if it matches (case‑insensitive).
            string properMatch = properNouns.FirstOrDefault(p => string.Equals(p, word, StringComparison.OrdinalIgnoreCase));
            if (properMatch != null)
            {
                words[i] = properMatch; // use the exact casing from the list
            }
            else if (word.Length > 0)
            {
                // Title case: first letter upper, rest lower.
                words[i] = char.ToUpper(word[0]) + word.Substring(1).ToLower();
            }
        }
        return string.Join(" ", words);
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Define proper nouns that should retain their original casing.
        var properNouns = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Aspose",
            "PDF",
            "PDF/A",
            "PDF/UA",
            "HTML",
            "XML"
        };

        // Initialize the bookmark editor and bind the PDF.
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Extract all existing bookmarks.
        Aspose.Pdf.Facades.Bookmarks bookmarks = editor.ExtractBookmarks();

        // Iterate each bookmark, compute the new title, and apply the modification.
        foreach (Aspose.Pdf.Facades.Bookmark bm in bookmarks)
        {
            string oldTitle = bm.Title;
            string newTitle = ToTitleCase(oldTitle, properNouns);
            if (!string.Equals(oldTitle, newTitle, StringComparison.Ordinal))
            {
                editor.ModifyBookmarks(oldTitle, newTitle);
            }
        }

        // Save the updated PDF.
        editor.Save(outputPdf);
        editor.Close(); // optional cleanup
        Console.WriteLine($"Bookmarks updated and saved to '{outputPdf}'.");
    }
}
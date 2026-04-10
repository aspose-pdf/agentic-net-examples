using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

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

        // List of proper nouns with the desired casing
        var properNouns = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "NASA",
            "Aspose",
            "PDF",
            "PDF/A",
            "PDF/X"
        };

        // Initialize the bookmark editor and bind the PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all bookmarks from the document
        Aspose.Pdf.Facades.Bookmarks bookmarks = editor.ExtractBookmarks();

        // Update each bookmark title to title case, preserving proper nouns
        foreach (Aspose.Pdf.Facades.Bookmark bm in bookmarks)
        {
            string originalTitle = bm.Title;
            string updatedTitle = ToTitleCaseWithProperNouns(originalTitle, properNouns);

            if (!originalTitle.Equals(updatedTitle, StringComparison.Ordinal))
            {
                editor.ModifyBookmarks(originalTitle, updatedTitle);
            }
        }

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Bookmarks updated and saved to '{outputPath}'.");
    }

    // Converts a string to title case while keeping defined proper nouns unchanged
    static string ToTitleCaseWithProperNouns(string text, HashSet<string> properNouns)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        string[] words = text.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            if (properNouns.Contains(word))
            {
                // Preserve the exact casing defined in the proper nouns set
                foreach (var pn in properNouns)
                {
                    if (pn.Equals(word, StringComparison.OrdinalIgnoreCase))
                    {
                        words[i] = pn;
                        break;
                    }
                }
            }
            else if (word.Length > 0)
            {
                // Title case: first character uppercase, the rest lowercase
                words[i] = char.ToUpperInvariant(word[0]) + word.Substring(1).ToLowerInvariant();
            }
        }
        return string.Join(" ", words);
    }
}
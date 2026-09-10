using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal PDF with a bookmark so the example can run in a sandbox
        // ---------------------------------------------------------------------
        CreateSamplePdfWithBookmark(inputPath);

        // List of proper nouns that should retain their original casing
        var properNouns = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "NASA",
            "ASP.NET",
            "PDF",
            "Aspose"
        };

        // Bind the PDF file to the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all bookmarks from the document
        var bookmarks = editor.ExtractBookmarks();

        // Iterate through each bookmark, convert its title to title case,
        // preserving proper nouns, and apply the modification
        foreach (var bm in bookmarks)
        {
            string newTitle = ToTitleCaseWithProperNouns(bm.Title, properNouns);
            // ModifyBookmarks replaces the old title with the new one
            editor.ModifyBookmarks(bm.Title, newTitle);
        }

        // Save the updated PDF with modified bookmark titles
        editor.Save(outputPath);
    }

    // Creates a one‑page PDF that contains a single bookmark (outline entry).
    private static void CreateSamplePdfWithBookmark(string path)
    {
        using var doc = new Document();
        doc.Pages.Add();
        // Add a simple outline item so there is something to rename.
        var outline = new OutlineItemCollection(doc.Outlines) { Title = "sample bookmark" };
        doc.Outlines.Add(outline);
        doc.Save(path);
    }

    // Converts a string to title case while keeping proper nouns unchanged
    static string ToTitleCaseWithProperNouns(string text, HashSet<string> properNouns)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];

            // Preserve the exact casing of known proper nouns
            if (properNouns.Contains(word))
            {
                foreach (var pn in properNouns)
                {
                    if (string.Equals(pn, word, StringComparison.OrdinalIgnoreCase))
                    {
                        word = pn;
                        break;
                    }
                }
            }
            else
            {
                // Standard title case: first letter uppercase, rest lowercase
                if (word.Length > 1)
                    word = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                else
                    word = word.ToUpper();
            }

            words[i] = word;
        }

        return string.Join(" ", words);
    }
}

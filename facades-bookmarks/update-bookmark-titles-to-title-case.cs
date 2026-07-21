using System;
using System.Collections.Generic;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Converts a bookmark title to title case while preserving predefined proper nouns.
    static string ToTitleCase(string original, HashSet<string> properNouns)
    {
        if (string.IsNullOrWhiteSpace(original))
            return original;

        // Split on whitespace; keep delimiters to reconstruct the string.
        var words = original.Split(new[] { ' ' }, StringSplitOptions.None);
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            if (string.IsNullOrEmpty(word))
                continue;

            // Preserve proper noun case if it matches (case‑insensitive).
            string wordUpper = word.ToUpperInvariant();
            if (properNouns.Contains(wordUpper))
            {
                // Use the proper noun as defined in the set (original case may differ).
                foreach (var pn in properNouns)
                {
                    if (pn.Equals(wordUpper, StringComparison.OrdinalIgnoreCase))
                    {
                        words[i] = pn; // proper noun entry is stored in its desired case.
                        break;
                    }
                }
                continue;
            }

            // Normal title‑case conversion: first letter upper, rest lower.
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            words[i] = ti.ToTitleCase(word.ToLowerInvariant());
        }

        return string.Join(" ", words);
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal PDF with a bookmark so the example can run in a sandbox
        // that has no pre‑existing files.
        // ---------------------------------------------------------------------
        using (var seed = new Document())
        {
            seed.Pages.Add(); // add a blank page
            // Create a bookmark (outline) named "sample bookmark".
            var outline = new OutlineItemCollection(seed.Outlines) { Title = "sample bookmark" };
            seed.Outlines.Add(outline);
            seed.Save(inputPdf);
        }

        // Define proper nouns (case as they should appear in the final title).
        var properNouns = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "NASA",
            "Aspose",
            "PDF"
        };

        // Initialize the bookmark editor and bind the source PDF.
        var editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Extract all bookmarks from the document.
        Bookmarks bookmarks = editor.ExtractBookmarks();

        // Iterate through each bookmark, compute the new title, and apply the change.
        foreach (Bookmark bm in bookmarks)
        {
            string oldTitle = bm.Title;
            string newTitle = ToTitleCase(oldTitle, properNouns);

            if (!string.Equals(oldTitle, newTitle, StringComparison.Ordinal))
            {
                // ModifyBookmarks updates the title of the matching bookmark.
                editor.ModifyBookmarks(oldTitle, newTitle);
            }
        }

        // Save the updated PDF.
        editor.Save(outputPdf);
    }
}

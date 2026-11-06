using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // Bookmark and Bookmarks are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string pattern = @"^Draft.*$"; // example regex: titles starting with "Draft"

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPdf);

            // Retrieve all top‑level bookmarks using the correct facade method
            Bookmarks topBookmarks = bookmarkEditor.ExtractBookmarks();

            // Compile the regular expression once
            Regex regex = new Regex(pattern, RegexOptions.Compiled);

            // Recursively process bookmarks and delete those whose Title matches the pattern
            foreach (Bookmark bm in topBookmarks)
            {
                DeleteMatchingBookmarks(bookmarkEditor, bm, regex);
            }

            // Save the modified PDF
            bookmarkEditor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks matching '{pattern}' have been removed. Saved to '{outputPdf}'.");
    }

    // Recursively walks the bookmark tree, deleting any bookmark whose Title matches the regex.
    static void DeleteMatchingBookmarks(PdfBookmarkEditor editor, Bookmark bookmark, Regex regex)
    {
        if (regex.IsMatch(bookmark.Title))
        {
            // DeleteBookmarks(string title) removes the bookmark and all its descendants.
            editor.DeleteBookmarks(bookmark.Title);
            // No need to traverse children because they are already removed.
            return;
        }

        // Process child bookmarks (if any). In the facade model children are accessed via ChildItems.
        if (bookmark.ChildItems != null)
        {
            // Clone the collection to avoid modification issues during recursion.
            Bookmarks children = new Bookmarks();
            foreach (Bookmark child in bookmark.ChildItems)
                children.Add(child);

            foreach (Bookmark child in children)
                DeleteMatchingBookmarks(editor, child, regex);
        }
    }
}

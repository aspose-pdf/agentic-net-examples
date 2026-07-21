using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_collapsed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all bookmarks from the document
        Bookmarks bookmarks = editor.ExtractBookmarks();

        // Collapse specific bookmarks (example: titles starting with "Chapter")
        foreach (Bookmark bm in bookmarks)
        {
            CollapseIfMatch(bm, title => title.StartsWith("Chapter"));
        }

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks collapsed and saved to '{outputPath}'.");
    }

    // Recursively set Open = false for bookmarks that satisfy the predicate
    static void CollapseIfMatch(Bookmark bm, Func<string, bool> predicate)
    {
        if (bm == null) return;

        if (predicate(bm.Title))
        {
            bm.Open = false; // collapsed state
        }

        // Process child bookmarks if any
        if (bm.ChildItems != null && bm.ChildItems.Count > 0)
        {
            foreach (Bookmark child in bm.ChildItems)
            {
                CollapseIfMatch(child, predicate);
            }
        }
    }
}
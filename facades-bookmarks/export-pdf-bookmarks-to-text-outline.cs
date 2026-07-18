using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "bookmarks.txt";

        // ------------------------------------------------------------
        // Create a sample PDF with a couple of bookmarks so the demo
        // works in a sandbox that has no pre‑existing files.
        // ------------------------------------------------------------
        CreateSamplePdfWithBookmarks(inputPdfPath);

        // Initialize the bookmark editor and bind the PDF file
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPdfPath);

        // Extract all bookmarks (including nested ones)
        Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

        // Write the outline to a plain‑text file with indentation reflecting hierarchy
        using (StreamWriter writer = new StreamWriter(outputTxtPath))
        {
            WriteBookmarksRecursive(bookmarks, writer, 0);
        }

        Console.WriteLine($"Bookmarks exported to '{outputTxtPath}'.");
    }

    // Helper that creates a minimal PDF containing a couple of hierarchical bookmarks
    private static void CreateSamplePdfWithBookmarks(string path)
    {
        using (Document doc = new Document())
        {
            // Add a single blank page
            doc.Pages.Add();

            // Create top‑level bookmark
            OutlineItemCollection chapter1 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Chapter 1"
            };
            doc.Outlines.Add(chapter1);

            // Add a child bookmark under Chapter 1
            // NOTE: The constructor always receives the root OutlineCollection (doc.Outlines).
            // The child is then attached to its parent via the Add method.
            OutlineItemCollection section1 = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Section 1.1"
            };
            chapter1.Add(section1);

            // Save the PDF so it can be read later by PdfBookmarkEditor
            doc.Save(path);
        }
    }

    // Recursively writes bookmarks, adding indentation for each level
    static void WriteBookmarksRecursive(Bookmarks bookmarks, StreamWriter writer, int level)
    {
        const int IndentSpaces = 4; // Number of spaces per hierarchy level

        foreach (Bookmark bm in bookmarks)
        {
            string indent = new string(' ', level * IndentSpaces);
            writer.WriteLine($"{indent}{bm.Title}");

            // Use the non‑obsolete ChildItems property
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                WriteBookmarksRecursive(bm.ChildItems, writer, level + 1);
            }
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "nested_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Create nested bookmarks ----------
        // Bind the source PDF to the editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Create child bookmarks
        Bookmark child1 = new Bookmark
        {
            Title = "Section 1.1",
            PageNumber = 2
        };

        Bookmark child2 = new Bookmark
        {
            Title = "Section 1.2",
            PageNumber = 3
        };

        // Collect child bookmarks
        Bookmarks childCollection = new Bookmarks();
        childCollection.Add(child1);
        childCollection.Add(child2);

        // Create parent bookmark and attach children
        Bookmark parent = new Bookmark
        {
            Title = "Chapter 1",
            PageNumber = 1,
            ChildItems = childCollection // forms the hierarchy
        };

        // Insert the hierarchy into the PDF
        editor.CreateBookmarks(parent);
        editor.Save(outputPath);
        editor.Close();

        // ---------- Extract and display the hierarchy ----------
        PdfBookmarkEditor extractor = new PdfBookmarkEditor();
        extractor.BindPdf(outputPath);
        Bookmarks allBookmarks = extractor.ExtractBookmarks();
        PrintBookmarks(allBookmarks, 0);
        extractor.Close();
    }

    // Recursive helper to print bookmark tree
    static void PrintBookmarks(Bookmarks bookmarks, int level)
    {
        string indent = new string(' ', level * 2);
        foreach (Bookmark bm in bookmarks)
        {
            Console.WriteLine($"{indent}- {bm.Title} (Page {bm.PageNumber})");
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                PrintBookmarks(bm.ChildItems, level + 1);
            }
        }
    }
}
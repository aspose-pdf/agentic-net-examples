using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Recursively prints bookmark hierarchy
    static void PrintBookmarks(Bookmarks bookmarks, int indentLevel)
    {
        string indent = new string(' ', indentLevel * 2);
        foreach (Bookmark bm in bookmarks)
        {
            Console.WriteLine($"{indent}- {bm.Title} (Page {bm.PageNumber})");
            // If the bookmark has children, recurse
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                PrintBookmarks(bm.ChildItems, indentLevel + 1);
            }
        }
    }

    static void Main()
    {
        const string basePdfPath = "sample.pdf";
        const string bookmarkedPdfPath = "sample_with_bookmarks.pdf";

        // -------------------------------------------------
        // Step 1: Create a simple PDF with three pages
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add three blank pages
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();

            // Save the base PDF
            doc.Save(basePdfPath);
        }

        // -------------------------------------------------
        // Step 2: Create nested bookmarks using PdfBookmarkEditor
        // -------------------------------------------------
        // Initialize the facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(basePdfPath);

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

        // Collect children into a Bookmarks collection
        Bookmarks childCollection = new Bookmarks();
        childCollection.Add(child1);
        childCollection.Add(child2);

        // Create parent bookmark and attach children
        Bookmark parent = new Bookmark
        {
            Title = "Chapter 1",
            PageNumber = 1,
            ChildItems = childCollection   // forms the nested hierarchy
        };

        // Add the hierarchy to the document
        editor.CreateBookmarks(parent);

        // Save the PDF with the new bookmark structure
        editor.Save(bookmarkedPdfPath);
        editor.Close();

        // -------------------------------------------------
        // Step 3: Extract and display the bookmark hierarchy
        // -------------------------------------------------
        PdfBookmarkEditor reader = new PdfBookmarkEditor();
        reader.BindPdf(bookmarkedPdfPath);

        // Extract all bookmarks (recursive)
        Bookmarks allBookmarks = reader.ExtractBookmarks();

        Console.WriteLine("Extracted Bookmark Hierarchy:");
        PrintBookmarks(allBookmarks, 0);

        reader.Close();
    }
}
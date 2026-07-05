using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath);
        }

        // Initialize the bookmark editor and bind the source PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // ----- Build hierarchical bookmarks -----
            // Parent bookmark representing Chapter 1
            Bookmark chapter1 = new Bookmark
            {
                Title = "Chapter 1",
                PageNumber = 1,
                Action = "GoTo"
            };

            // Child bookmarks under Chapter 1
            Bookmark section11 = new Bookmark
            {
                Title = "Section 1.1",
                PageNumber = 2,
                Action = "GoTo"
            };
            Bookmark section12 = new Bookmark
            {
                Title = "Section 1.2",
                PageNumber = 3,
                Action = "GoTo"
            };

            // Attach child items to the parent bookmark using the proper API (add to the existing collection)
            chapter1.ChildItems.Add(section11);
            chapter1.ChildItems.Add(section12);

            // Another top‑level bookmark (Chapter 2) without children
            Bookmark chapter2 = new Bookmark
            {
                Title = "Chapter 2",
                PageNumber = 4,
                Action = "GoTo"
            };

            // Add the hierarchical bookmarks to the PDF
            editor.CreateBookmarks(chapter1); // adds Chapter 1 and its children
            editor.CreateBookmarks(chapter2); // adds Chapter 2

            // Save the updated PDF with the new bookmark structure
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks created and saved to '{outputPath}'.");
    }

    private static void CreateSamplePdf(string path)
    {
        // Create a 4‑page PDF with simple text so that the bookmark page numbers are valid.
        using (Document doc = new Document())
        {
            for (int i = 1; i <= 4; i++)
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment($"Page {i}"));
            }
            doc.Save(path);
        }
    }
}

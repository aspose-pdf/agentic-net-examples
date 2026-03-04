using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the bookmark editor and bind the loaded document
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
            bookmarkEditor.BindPdf(doc);

            // ----- Build nested bookmark hierarchy -----
            // Child bookmarks under the first parent
            Bookmark child1 = new Bookmark
            {
                Title = "Chapter 1.1",
                PageNumber = 2,
                Action = "GoTo"
            };
            Bookmark child2 = new Bookmark
            {
                Title = "Chapter 1.2",
                PageNumber = 3,
                Action = "GoTo"
            };
            // Collection of child bookmarks
            Bookmarks childCollection = new Bookmarks();
            childCollection.Add(child1);
            childCollection.Add(child2);

            // Parent bookmark that contains the children
            Bookmark parent = new Bookmark
            {
                Title = "Chapter 1",
                PageNumber = 1,
                Action = "GoTo",
                ChildItem = childCollection
            };

            // Another top‑level bookmark without children
            Bookmark appendix = new Bookmark
            {
                Title = "Appendix",
                PageNumber = 5,
                Action = "GoTo"
            };

            // Top‑level collection of bookmarks
            Bookmarks topLevel = new Bookmarks();
            topLevel.Add(parent);
            topLevel.Add(appendix);

            // Add each top‑level bookmark (with its nested children) to the PDF
            foreach (Bookmark bm in topLevel)
            {
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the PDF with the new bookmark hierarchy
            bookmarkEditor.Save(outputPath);
            // Release resources held by the editor
            bookmarkEditor.Close();
        }

        Console.WriteLine($"Nested bookmarks created and saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Create child bookmarks
            Bookmark child1 = new Bookmark
            {
                Title = "Chapter 1",
                PageNumber = 1
            };

            Bookmark child2 = new Bookmark
            {
                Title = "Chapter 2",
                PageNumber = 2
            };

            // Assemble child bookmarks into a collection
            Bookmarks childCollection = new Bookmarks();
            childCollection.Add(child1);
            childCollection.Add(child2);

            // Create a parent bookmark and attach the child collection
            Bookmark parent = new Bookmark
            {
                Title = "Contents",
                PageNumber = 1,
                ChildItems = childCollection,
                BoldFlag = true   // optional styling
            };

            // Add the hierarchical bookmark structure to the PDF
            editor.CreateBookmarks(parent);

            // Save the modified PDF with nested bookmarks
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with nested bookmarks saved to '{outputPath}'.");
    }
}
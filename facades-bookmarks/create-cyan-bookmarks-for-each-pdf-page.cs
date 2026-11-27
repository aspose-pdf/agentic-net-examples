using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the number of pages using a Document (disposed via using)
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Create a PdfBookmarkEditor, bind the PDF and add cyan bookmarks for each page
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            for (int i = 1; i <= pageCount; i++)
            {
                // Build a single Bookmark instance with the desired properties
                Bookmark bm = new Bookmark
                {
                    Title = $"Page {i}",
                    PageNumber = i,
                    TitleColor = System.Drawing.Color.Cyan, // fully qualified to avoid ambiguity
                    Action = "GoTo" // required action string for a regular bookmark
                };

                // Create the bookmark in the PDF (CreateBookmarks accepts a single Bookmark)
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the PDF with the new bookmarks
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}

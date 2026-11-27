using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the bookmark editor and bind the PDF
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);

        // Build a bookmark that points to page 8 (1‑based indexing)
        Bookmark summaryBookmark = new Bookmark();
        summaryBookmark.Title = "Summary";   // bookmark title
        summaryBookmark.PageNumber = 8;       // target page number
        summaryBookmark.Action = "GoTo";    // required action string for a page link

        // Add the bookmark to the document using the correct API method
        bookmarkEditor.CreateBookmarks(summaryBookmark);

        // Save the modified PDF
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}

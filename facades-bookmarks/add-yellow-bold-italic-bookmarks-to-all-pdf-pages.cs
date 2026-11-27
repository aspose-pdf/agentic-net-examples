using System;
using System.IO;
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

        // Initialize the bookmark editor and bind the source PDF
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Get the total number of pages via the underlying Document object
            int pageCount = bookmarkEditor.Document.Pages.Count;

            // Create a yellow, bold, italic bookmark for each page
            for (int i = 1; i <= pageCount; i++)
            {
                Bookmark bm = new Bookmark
                {
                    Title = $"Page {i}",
                    PageNumber = i,
                    // Fully qualify the System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
                    TitleColor = System.Drawing.Color.Yellow,
                    BoldFlag = true,
                    ItalicFlag = true
                };

                // Add the bookmark to the PDF
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the updated PDF with the new bookmarks
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}

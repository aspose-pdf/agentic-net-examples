using System;
using System.IO;
using System.Drawing;
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

        // Obtain the number of pages via PdfFileInfo (or Document.Pages.Count)
        int pageCount;
        using (var info = new PdfFileInfo(inputPath))
        {
            pageCount = info.NumberOfPages;
        }

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block
        using (var bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF document
            bookmarkEditor.BindPdf(inputPath);

            // Add a black‑colored regular bookmark for each page (1‑based indexing)
            for (int i = 1; i <= pageCount; i++)
            {
                var bm = new Bookmark
                {
                    Title = $"Page {i}",
                    PageNumber = i,
                    Level = 1,               // top‑level bookmark
                    TitleColor = System.Drawing.Color.Black // fully qualified to avoid ambiguity
                };
                // Create (add) the bookmark – the facade accepts a single Bookmark instance
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the PDF with the new bookmarks
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}

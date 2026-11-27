using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the number of pages in the source PDF.
        int pageCount;
        using (var doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count; // 1‑based page count
        }

        // PdfBookmarkEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        var bookmarkEditor = new PdfBookmarkEditor();

        // Load (bind) the PDF to the editor.
        bookmarkEditor.BindPdf(inputPath);

        // Create a top‑level bookmark for each page.
        for (int i = 1; i <= pageCount; i++)
        {
            // Create a new bookmark instance with the required visual style.
            var bm = new Bookmark
            {
                Title       = $"Page {i}",
                PageNumber  = i,
                ItalicFlag = true,                     // italic style
                TitleColor  = System.Drawing.Color.Gray // gray colour for the bookmark text
            };

            // Add the bookmark to the document.
            bookmarkEditor.CreateBookmarks(bm);
        }

        // Save the PDF with the newly added bookmarks.
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}

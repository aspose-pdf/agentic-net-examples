using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to obtain the page count.
        Document doc = new Document(inputPath);
        try
        {
            // PdfBookmarkEditor does NOT implement IDisposable, so we instantiate it normally.
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the editor to the PDF file.
            bookmarkEditor.BindPdf(inputPath);

            // Create a cyan‑colored, bold bookmark for each page.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Bookmark bm = new Bookmark
                {
                    Title      = $"Page {i}",
                    PageNumber = i,
                    BoldFlag   = true,
                    TitleColor = System.Drawing.Color.Cyan // fully qualified to avoid ambiguity
                };
                // Add the bookmark to the document.
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the PDF with the new bookmarks.
            bookmarkEditor.Save(outputPath);
            // Optionally close the editor (releases file handles).
            bookmarkEditor.Close();
        }
        finally
        {
            doc.Dispose();
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}

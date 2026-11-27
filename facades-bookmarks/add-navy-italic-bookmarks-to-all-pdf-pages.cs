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
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the document to obtain the number of pages.
            Document pdfDoc = new Document(inputPath);
            int pageCount = pdfDoc.Pages.Count;

            // PdfBookmarkEditor does not implement IDisposable.
            PdfBookmarkEditor editor = new PdfBookmarkEditor();

            // Bind the PDF (can bind by path or by Document instance).
            editor.BindPdf(pdfDoc);

            // Create a navy‑colored italic bookmark for each page.
            for (int i = 1; i <= pageCount; i++)
            {
                Bookmark bm = new Bookmark
                {
                    Title      = $"Page {i}",
                    PageNumber = i,
                    ItalicFlag = true,
                    // Fully qualify the System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
                    TitleColor = System.Drawing.Color.Navy
                };

                // Add as a top‑level bookmark.
                editor.CreateBookmarks(bm);
            }

            // Save the modified PDF.
            editor.Save(outputPath);
            Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

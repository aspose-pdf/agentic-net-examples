using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_bookmarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, so we manage its lifetime manually.
            PdfBookmarkEditor editor = new PdfBookmarkEditor();

            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Create a single Bookmark instance.
            Bookmark bookmark = new Bookmark
            {
                Title = "Page Six",
                PageNumber = 6,          // target page number (1‑based)
                Action = "GoTo"        // action type as a string
            };

            // Add the bookmark to the document.
            editor.CreateBookmarks(bookmark);

            // Save the modified PDF.
            editor.Save(outputPdf);

            // Close the editor (releases internal resources).
            editor.Close();

            Console.WriteLine($"Bookmark added and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

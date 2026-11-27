using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "multiBookmark.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Use PdfBookmarkEditor (not PdfContentEditor) to work with bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(sourcePath);

            // Add multiple bookmarks. Page numbers are 1‑based.
            editor.CreateBookmarkOfPage("Chapter 1", 1);
            editor.CreateBookmarkOfPage("Chapter 2", 5);
            editor.CreateBookmarkOfPage("Appendix", 10);

            // Save the PDF with the new bookmarks.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}

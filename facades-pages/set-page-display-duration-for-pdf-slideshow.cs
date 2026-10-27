using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfPageEditor resides here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "slideshow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document only to obtain the total page count
        Document pdfDocument = new Document(inputPath);
        int pageCount = pdfDocument.Pages.Count;

        // Prepare an array with all page numbers (1‑based indexing)
        int[] allPages = Enumerable.Range(1, pageCount).ToArray();

        // Use PdfPageEditor to set the display duration for the selected pages
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            editor.ProcessPages = allPages;          // apply to every page
            editor.DisplayDuration = 5;              // duration in seconds (int, not double)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Slideshow PDF saved to '{outputPath}'.");
    }
}

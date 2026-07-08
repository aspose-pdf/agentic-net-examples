using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // PDF with the new bookmark

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the existing PDF.
            editor.BindPdf(inputPdf);

            // Create a bookmark titled "Project Overview" that points to page 5.
            // Page numbers are 1‑based in Aspose.Pdf.
            editor.CreateBookmarkOfPage("Project Overview", 5);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPdf}'.");
    }
}
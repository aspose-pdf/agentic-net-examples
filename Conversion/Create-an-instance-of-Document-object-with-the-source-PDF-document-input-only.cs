using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (input‑only) inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Document is now loaded and can be used for read‑only operations
            Console.WriteLine($"Loaded PDF with {pdfDoc.Pages.Count} pages.");
        }
    }
}
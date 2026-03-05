using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (input‑only) using a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example operation: display the number of pages in the loaded document
            Console.WriteLine($"Loaded PDF with {pdfDoc.Pages.Count} pages.");
        }
    }
}
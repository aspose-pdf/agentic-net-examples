using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where individual page PDFs will be saved
        const string outputDir = "ExtractedPages";

        // Pages to extract (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToExtract = { 2, 4, 7 }; // example selection

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Create an instance of PdfFileEditor (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        foreach (int pageNumber in pagesToExtract)
        {
            // Build output file name for the current page
            string outputPath = Path.Combine(outputDir, $"page{pageNumber}.pdf");

            // Extract the single page (startPage == endPage)
            bool success = editor.Extract(inputPdf, pageNumber, pageNumber, outputPath);

            if (success)
                Console.WriteLine($"Page {pageNumber} extracted to '{outputPath}'.");
            else
                Console.Error.WriteLine($"Failed to extract page {pageNumber}.");
        }

        // No need to dispose PdfFileEditor; it does not implement IDisposable
    }
}
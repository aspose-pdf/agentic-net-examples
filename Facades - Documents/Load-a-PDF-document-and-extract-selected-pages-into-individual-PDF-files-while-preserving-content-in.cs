using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where individual page PDFs will be saved
        const string outputDir = "ExtractedPages";

        // List of page numbers to extract (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToExtract = new int[] { 2, 4, 7 }; // example pages

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        foreach (int pageNumber in pagesToExtract)
        {
            // Construct output file name for the current page
            string outputPath = Path.Combine(outputDir, $"page{pageNumber}.pdf");

            try
            {
                // Extract the single page and save it as a new PDF
                // The Extract method expects an array of page numbers (1‑based)
                bool success = editor.Extract(inputPdf, new int[] { pageNumber }, outputPath);

                if (success)
                {
                    Console.WriteLine($"Page {pageNumber} extracted to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to extract page {pageNumber}.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error extracting page {pageNumber}: {ex.Message}");
            }
        }
    }
}
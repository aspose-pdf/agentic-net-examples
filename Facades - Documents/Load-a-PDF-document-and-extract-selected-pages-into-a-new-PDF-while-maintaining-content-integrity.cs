using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output PDF file path (will contain the extracted pages)
        const string outputPdf = "extracted_pages.pdf";

        // Pages to extract – 1‑based page numbers as required by Aspose.Pdf
        int[] pagesToExtract = new int[] { 2, 4, 5 };

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Extract the specified pages and save them to the output file.
            // The method returns true on success; we can optionally check the return value.
            bool success = editor.Extract(inputPdf, pagesToExtract, outputPdf);

            if (success && File.Exists(outputPdf))
            {
                Console.WriteLine($"Pages extracted successfully to '{outputPdf}'.");
            }
            else
            {
                Console.Error.WriteLine("Extraction failed.");
            }
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., file access issues, corrupted PDF, etc.)
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
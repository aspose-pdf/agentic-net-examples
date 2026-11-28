using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "document.pdf";
        // Output PDF containing the extracted pages
        const string outputPath = "array_extracted.pdf";
        // Pages to extract (1‑based indexing)
        int[] pagesToExtract = new int[] { 2, 4, 6, 8, 10 };

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block
            PdfFileEditor editor = new PdfFileEditor();

            // Extract the specified pages and save to the output file
            editor.Extract(inputPath, pagesToExtract, outputPath);

            Console.WriteLine($"Pages extracted successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
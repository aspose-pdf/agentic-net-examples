using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (extracted pages will be saved here)
        const string outputPath = "extracted_pages.pdf";
        // Pages to extract (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToExtract = new int[] { 1, 3, 5 };

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade
        PdfFileEditor editor = new PdfFileEditor();

        // Extract the specified pages and save to the output file
        bool success = editor.Extract(inputPath, pagesToExtract, outputPath);

        if (success)
        {
            Console.WriteLine($"Pages extracted successfully to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to extract pages.");
        }
    }
}
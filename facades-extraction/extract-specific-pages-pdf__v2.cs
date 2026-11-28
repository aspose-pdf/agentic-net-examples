using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "document.pdf";
        const string outputPath = "specific_set.pdf";

        // Verify source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create PdfFileEditor (does NOT implement IDisposable, so no using block)
        PdfFileEditor editor = new PdfFileEditor();

        // Define the pages to extract (1‑based indexing)
        int[] pagesToExtract = new int[] { 3, 6, 9, 12 };

        try
        {
            // Extract the specified pages and save to the new PDF
            // NOTE: PdfFileEditor.Extract expects the order: sourcePath, pagesArray, outputPath
            editor.Extract(sourcePath, pagesToExtract, outputPath);
            Console.WriteLine($"Pages {string.Join(",", pagesToExtract)} extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}

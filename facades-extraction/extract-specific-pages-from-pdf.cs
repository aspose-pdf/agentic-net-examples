using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "document.pdf";
        const string outputPath = "specific_pages.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Aspose.Pdf uses 1‑based page numbers
        int[] pagesToExtract = new int[] { 2, 4, 6 };

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block
            PdfFileEditor editor = new PdfFileEditor();

            // Extract the specified pages and save to a new PDF
            // Correct argument order: source path, pages array, destination path
            editor.Extract(inputPath, pagesToExtract, outputPath);

            Console.WriteLine($"Pages {string.Join(", ", pagesToExtract)} extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting pages: {ex.Message}");
        }
    }
}

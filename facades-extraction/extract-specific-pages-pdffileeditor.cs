using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "array_pages.pdf";

        // Pages to extract (1‑based indexing)
        int[] pagesToExtract = new int[] { 7, 14, 21 };

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Correct argument order: source file, pages array, destination file
            editor.Extract(inputPath, pagesToExtract, outputPath);

            Console.WriteLine($"Pages {string.Join(", ", pagesToExtract)} extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors (e.g., invalid page numbers, I/O issues)
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

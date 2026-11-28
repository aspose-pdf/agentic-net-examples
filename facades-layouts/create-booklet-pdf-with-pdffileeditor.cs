using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "booklet_lr.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the page numbers for the left and right sides of the booklet.
        // Aspose.Pdf uses 1‑based page indexing, so page 1 is the first page.
        int[] leftPages  = new int[] { 1, 3, 5, 7 }; // example left‑hand pages
        int[] rightPages = new int[] { 2, 4, 6, 8 }; // example right‑hand pages

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Create the booklet using the specified page arrays.
            editor.MakeBooklet(inputPath, outputPath, leftPages, rightPages);
            Console.WriteLine($"Booklet created successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating booklet: {ex.Message}");
        }
    }
}
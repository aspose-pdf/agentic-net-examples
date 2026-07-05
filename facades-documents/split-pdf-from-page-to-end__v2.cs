using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Page number from which to start the split (1‑based indexing)
        const int startPage = 5;
        // Output PDF file path (the rear part of the document)
        const string outputPath = "rear_part.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the specified start page to the end of the document
            // The method returns true on success; we can optionally check the result
            bool success = editor.SplitToEnd(inputPath, startPage, outputPath);

            if (success)
                Console.WriteLine($"PDF successfully split. Rear part saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Split operation failed.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., invalid page number, I/O issues)
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}
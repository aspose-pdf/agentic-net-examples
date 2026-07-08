using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Page number to start splitting from (1‑based indexing)
        const int startPage = 3;
        // Output PDF file path (contains pages from startPage to the end)
        const string outputPath = "split_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor provides split operations; it does NOT implement IDisposable,
            // so we instantiate it directly without a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // SplitToEnd extracts the rear part of the document starting at startPage.
            // The method returns true on success; we can optionally check the result.
            bool success = editor.SplitToEnd(inputPath, startPage, outputPath);

            if (success)
                Console.WriteLine($"PDF split successfully. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("PDF split failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}
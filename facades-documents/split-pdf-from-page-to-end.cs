using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (contains pages from startPage to the end)
        const string outputPath = "split_output.pdf";
        // Page number from which to start the split (1‑based indexing)
        const int startPage = 3;

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Split from the specified start page to the end of the document
        // SplitToEnd returns true on success, false otherwise
        bool success = editor.SplitToEnd(inputPath, startPage, outputPath);

        if (success && File.Exists(outputPath))
        {
            Console.WriteLine($"PDF successfully split. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to split the PDF.");
        }
    }
}
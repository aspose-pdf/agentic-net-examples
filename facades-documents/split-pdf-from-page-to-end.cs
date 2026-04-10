using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (rear part after splitting)
        const string outputPath = "split_end.pdf";
        // Page number from which to start the split (1‑based indexing)
        const int startPage = 5;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; instantiate directly
        PdfFileEditor editor = new PdfFileEditor();

        // Split from the specified start page to the end of the document
        bool success = editor.SplitToEnd(inputPath, startPage, outputPath);

        if (success)
        {
            Console.WriteLine($"Successfully split PDF from page {startPage} to the end.");
            Console.WriteLine($"Result saved to: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Split operation failed.");
        }
    }
}
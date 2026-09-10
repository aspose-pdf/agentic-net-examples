using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (the split part from startPage to the end)
        const string outputPath = "output_split.pdf";
        // Page number from which to start the split (1‑based indexing)
        const int startPage = 3;

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor instance (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Split the document from startPage to the end and save to outputPath
        bool success = editor.SplitToEnd(inputPath, startPage, outputPath);

        if (success)
        {
            Console.WriteLine($"PDF successfully split. Pages {startPage}‑end saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to split the PDF file.");
        }
    }
}
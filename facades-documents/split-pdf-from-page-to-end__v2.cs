using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string inputPath = "input.pdf";
        // Desired output file that will contain pages from startPage to the end
        const string outputPath = "output_from_page5.pdf";
        // Page number from which to start the split (1‑based indexing)
        int startPage = 5;

        // Verify that the source file exists before attempting the operation
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so it is instantiated directly
        PdfFileEditor editor = new PdfFileEditor();

        // SplitToEnd returns true on success, false otherwise
        bool success = editor.SplitToEnd(inputPath, startPage, outputPath);

        if (success)
        {
            Console.WriteLine($"PDF successfully split from page {startPage} to the end.");
            Console.WriteLine($"Output saved to: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to split the PDF.");
        }
    }
}
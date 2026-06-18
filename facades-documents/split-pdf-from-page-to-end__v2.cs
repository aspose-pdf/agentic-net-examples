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
        // Output PDF file path that will contain pages startPage..end
        const string outputPath = "output_split.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade (does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the split operation; returns true on success
        bool success = editor.SplitToEnd(inputPath, startPage, outputPath);

        if (success)
        {
            Console.WriteLine($"PDF successfully split. Rear part saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Split operation failed.");
        }
    }
}
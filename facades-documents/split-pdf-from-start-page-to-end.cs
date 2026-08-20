using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path that will contain pages from startPage to the end
        const string outputPath = "rear_part.pdf";
        // Page number from which the split should start (1‑based indexing)
        const int startPage = 5;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF from startPage to the end and save the rear part
        bool success = editor.SplitToEnd(inputPath, startPage, outputPath);

        if (success)
        {
            Console.WriteLine($"PDF successfully split. Rear part saved to '{outputPath}'.");
        }
        else
        {
            Console.WriteLine("PDF split operation failed.");
        }
    }
}
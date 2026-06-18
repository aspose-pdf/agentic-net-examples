using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Page number up to which the PDF will be split (inclusive)
        const int endPage = 5;
        // Output PDF file path for the front part (pages 1..endPage)
        const string outputPath = "output_split.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor provides file‑level operations; it does not implement IDisposable
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the first page to the specified end page and save the result
            bool success = editor.SplitFromFirst(inputPath, endPage, outputPath);

            if (success)
                Console.WriteLine($"PDF successfully split. Front part saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("PDF split operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}
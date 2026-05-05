using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (front part of the split)
        const string outputPath = "output.pdf";
        // Page number up to which the PDF will be split (inclusive)
        const int endPage = 5;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the first page to the specified end page and save the front part
            bool success = editor.SplitFromFirst(inputPath, endPage, outputPath);

            if (success)
                Console.WriteLine($"PDF successfully split. Front part saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Split operation failed.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
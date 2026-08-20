using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (front part of the document)
        const string outputPath = "front_part.pdf";
        // Page number up to which the document will be split (inclusive)
        const int endPage = 5;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the first page to the specified location and save the front part
            bool success = editor.SplitFromFirst(inputPath, endPage, outputPath);

            if (success)
                Console.WriteLine($"Successfully extracted pages 1-{endPage} to '{outputPath}'.");
            else
                Console.WriteLine("SplitFromFirst returned false – operation may have failed.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}
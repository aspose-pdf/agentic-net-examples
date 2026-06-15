using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";
        // Path where the front part (pages 1..splitLocation) will be saved
        const string outputPath = "front_part.pdf";
        // Page number up to which the document will be split (inclusive)
        int splitLocation = 5;

        // Verify that the source file exists before attempting the operation
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block
            PdfFileEditor editor = new PdfFileEditor();

            // SplitFromFirst extracts pages from the first page up to 'splitLocation'
            // and saves them as a new PDF file.
            bool success = editor.SplitFromFirst(inputPath, splitLocation, outputPath);

            if (success)
                Console.WriteLine($"Successfully split first {splitLocation} pages to '{outputPath}'.");
            else
                Console.Error.WriteLine("Split operation failed.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., file access issues, corrupted PDF)
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
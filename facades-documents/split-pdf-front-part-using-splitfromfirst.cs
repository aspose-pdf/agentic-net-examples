using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (front part up to the specified page)
        const string outputPath = "front_part.pdf";
        // Page number up to which the PDF will be split (inclusive)
        const int endPage = 5;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Create an instance of PdfFileEditor (does NOT implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the first page to the specified end page and save the front part
            bool success = editor.SplitFromFirst(inputPath, endPage, outputPath);

            if (success)
                Console.WriteLine($"Successfully split pages 1-{endPage} to '{outputPath}'.");
            else
                Console.Error.WriteLine("Split operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
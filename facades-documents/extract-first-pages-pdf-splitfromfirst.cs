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
        const int splitLocation = 5;
        // Output PDF file that will contain pages 1..splitLocation
        const string outputPath = "front_part.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is required
            PdfFileEditor editor = new PdfFileEditor();

            // SplitFromFirst extracts pages from the first page up to 'splitLocation'
            // and saves the resulting document to 'outputPath'.
            bool success = editor.SplitFromFirst(inputPath, splitLocation, outputPath);

            if (success)
                Console.WriteLine($"Successfully created '{outputPath}' containing pages 1-{splitLocation}.");
            else
                Console.Error.WriteLine("Split operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path for the booklet
        const string outputPath = "booklet.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create an instance of PdfFileEditor (does not implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet using default settings
            bool result = editor.MakeBooklet(inputPath, outputPath);

            // Report the outcome
            if (result)
                Console.WriteLine($"Booklet created successfully: {outputPath}");
            else
                Console.Error.WriteLine("Failed to create booklet.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
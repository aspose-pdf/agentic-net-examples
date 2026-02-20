using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (booklet version)
        const string outputPath = "output_booklet.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor provides the MakeBooklet operation.
            // It does NOT implement IDisposable, so we instantiate it without a using block.
            PdfFileEditor editor = new PdfFileEditor();
            editor.MakeBooklet(inputPath, outputPath);
            Console.WriteLine($"Booklet created successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"Error while creating booklet: {ex.Message}");
        }
    }
}

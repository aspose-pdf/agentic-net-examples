using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the facade that provides booklet functionality
            PdfFileEditor editor = new PdfFileEditor();

            // Generate a booklet PDF; returns true on success
            bool result = editor.MakeBooklet(inputPath, outputPath);

            if (result)
                Console.WriteLine($"Booklet created successfully: {outputPath}");
            else
                Console.Error.WriteLine("Failed to create booklet.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
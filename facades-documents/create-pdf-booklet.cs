using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Create a booklet using default settings
            bool result = editor.MakeBooklet(inputPath, outputPath);

            Console.WriteLine(result
                ? $"Booklet successfully created at '{outputPath}'."
                : "Failed to create booklet.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
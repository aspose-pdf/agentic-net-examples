using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the facade object
            PdfFileEditor editor = new PdfFileEditor();

            // Generate a booklet using default settings
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
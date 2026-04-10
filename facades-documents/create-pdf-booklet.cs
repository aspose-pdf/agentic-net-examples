using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor is a facade class; it does not implement IDisposable,
        // so we instantiate it directly without a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Create a booklet using default settings.
        // The method returns true on success, false otherwise.
        bool result = editor.MakeBooklet(inputPath, outputPath);

        Console.WriteLine(result
            ? $"Booklet created successfully: {outputPath}"
            : "Failed to create booklet.");
    }
}
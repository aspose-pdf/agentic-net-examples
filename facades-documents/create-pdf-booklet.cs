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

        // PdfFileEditor does not implement IDisposable, so no using block is needed
        PdfFileEditor editor = new PdfFileEditor();

        // Create a booklet using default settings
        editor.MakeBooklet(inputPath, outputPath);

        Console.WriteLine($"Booklet created successfully: {outputPath}");
    }
}
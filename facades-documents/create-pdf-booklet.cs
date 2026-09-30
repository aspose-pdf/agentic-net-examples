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
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so no using block is required
        PdfFileEditor editor = new PdfFileEditor();

        // Create a booklet using default settings
        // MakeBooklet(sourceFile, destinationFile)
        editor.MakeBooklet(inputPath, outputPath);

        Console.WriteLine($"Booklet created: {outputPath}");
    }
}
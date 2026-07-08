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

        // PdfFileEditor does NOT implement IDisposable; instantiate directly
        var editor = new PdfFileEditor();
        bool result = editor.MakeBooklet(inputPath, outputPath);

        // Report the outcome
        Console.WriteLine(result
            ? $"Booklet successfully created at '{outputPath}'."
            : "Failed to create booklet.");
    }
}

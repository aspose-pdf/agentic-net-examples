using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        var editor = new PdfFileEditor();

        // Passing null for the page array applies the operation to all pages.
        editor.AddMargins(inputPath, outputPath, null, 10, 10, 10, 10);

        Console.WriteLine($"Transformed PDF saved to '{outputPath}'.");
    }
}

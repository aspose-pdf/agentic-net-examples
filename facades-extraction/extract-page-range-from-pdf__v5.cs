using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "range12_18.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Extract(inputPath, 12, 18, outputPath);

        Console.WriteLine($"Pages 12-18 extracted to '{outputPath}'.");
    }
}
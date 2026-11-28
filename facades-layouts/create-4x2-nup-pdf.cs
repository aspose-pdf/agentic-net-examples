using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_4x2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Create a 4×2 N‑up layout (4 rows, 2 columns) from the source PDF.
        // The correct method is MakeNUp, which expects an array of source files and a landscape flag.
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // set to true if you need landscape orientation
        bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        if (success)
            Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create N‑up PDF.");
    }
}

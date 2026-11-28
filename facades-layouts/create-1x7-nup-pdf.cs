using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_1x7.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // MakeNUp expects an array of source PDF file paths and a landscape flag.
        // For a single‑source 1×7 layout we pass the single file and set landscape as needed.
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // change to true if a landscape layout is required

        bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);
        if (!success)
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
            return;
        }

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}

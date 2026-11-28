using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_2x6.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Use MakeNUp (the correct API) to create an N‑up layout.
        // It expects an array of source PDF file paths, the output path, and a flag indicating
        // whether the resulting pages should be in landscape orientation.
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // change to true if a landscape layout is required
        bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        if (success)
            Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create N‑up PDF.");
    }
}

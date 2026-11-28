using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_2x3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Use MakeNUp instead of the non‑existent NUp method.
        // MakeNUp expects an array of source PDF file paths, the destination path,
        // and a flag indicating whether the resulting N‑up layout should be landscape.
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // set to true if a landscape orientation is required
        bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        if (success)
            Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create N‑up PDF.");
    }
}

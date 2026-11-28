using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_5x5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // MakeNUp expects an array of source PDFs, the output path, and a landscape flag.
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // 5×5 layout is portrait by default

        bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        if (success)
            Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create N‑up PDF.");
    }
}

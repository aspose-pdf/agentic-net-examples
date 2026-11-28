using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_2x4.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; instantiate directly
        PdfFileEditor editor = new PdfFileEditor();

        // Use MakeNUp (the correct API) – it expects an array of source files,
        // the output path, and a flag indicating whether the layout should be landscape.
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // change to true if a landscape N‑up is required
        bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        if (success)
            Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create N‑up PDF.");
    }
}

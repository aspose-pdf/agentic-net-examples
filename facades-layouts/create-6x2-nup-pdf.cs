using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_6x2.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so no using block is needed.
        // Use MakeNUp instead of the non‑existent NUp method.
        PdfFileEditor editor = new PdfFileEditor();
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // set true if you want a landscape layout
        bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);
        if (!success)
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
            return;
        }

        Console.WriteLine($"N‑up PDF created: {outputPath}");
    }
}

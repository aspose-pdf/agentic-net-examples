using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_6x6.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable – do NOT use a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Create a 6×6 N‑up layout using the correct API (MakeNUp).
        // MakeNUp expects an array of source PDFs and a landscape flag.
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // keep original orientation
        bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        if (success)
            Console.WriteLine($"N‑up PDF created: {outputPath}");
        else
            Console.Error.WriteLine("Failed to create N‑up PDF.");
    }
}

using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_5x1.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();

        // Create a 5×1 N‑up layout (5 rows, 1 column).
        // Use MakeNUp which expects an array of source files and a landscape flag.
        string[] sourceFiles = new[] { inputPath };
        bool success = editor.MakeNUp(sourceFiles, outputPath, false); // false = keep original orientation

        if (!success)
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
            return;
        }

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
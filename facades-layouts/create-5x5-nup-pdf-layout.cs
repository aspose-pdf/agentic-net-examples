using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "large.pdf";
        const string outputPath = "nup_5x5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Create a 5‑by‑5 N‑up layout (5 rows, 5 columns).
        // MakeNUp(string inputFile, string outputFile, int rows, int columns)
        editor.MakeNUp(inputPath, outputPath, 5, 5);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
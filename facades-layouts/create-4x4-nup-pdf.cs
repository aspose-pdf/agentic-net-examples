using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_4x4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Aspose.Pdf.Facades does not provide an NUp method. The correct API is MakeNUp,
            // which accepts an array of source PDF file paths, the output path, and a flag
            // indicating whether the resulting pages should be landscape.
            // For a single‑source 4×4 N‑up layout we pass the source file in an array.
            string[] sourceFiles = new[] { inputPath };
            bool isLandscape = false; // change to true if a landscape orientation is required
            editor.MakeNUp(sourceFiles, outputPath, isLandscape);

            Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during N‑up operation: {ex.Message}");
        }
    }
}

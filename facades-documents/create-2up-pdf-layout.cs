using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_2up.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Create a 2‑up layout (two pages per sheet). The MakeNUp overload expects:
        //   1. An array of source PDF file paths.
        //   2. The output PDF file path.
        //   3. A bool indicating whether the resulting pages should be in landscape orientation.
        // Here we use portrait orientation (isLandscape = false).
        bool success = editor.MakeNUp(new[] { inputPath }, outputPath, false);

        if (success)
            Console.WriteLine($"2‑up PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create 2‑up PDF.");
    }
}

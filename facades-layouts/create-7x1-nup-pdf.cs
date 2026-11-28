using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_7x1.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Use MakeNUp (the correct method) to create a 7×1 N‑up layout.
            // The method expects an array of source PDF paths, the output path, and a
            // landscape flag. For a single‑source PDF we pass a one‑element array.
            string[] sourceFiles = new[] { inputPath };
            bool isLandscape = false; // portrait orientation for 7 rows × 1 column
            bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);

            if (success)
                Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("N‑up operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during N‑up operation: {ex.Message}");
        }
    }
}

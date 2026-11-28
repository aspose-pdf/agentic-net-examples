using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_2x2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
            PdfFileEditor editor = new PdfFileEditor();

            // Aspose.Pdf.Facades does not have an NUp method. Use MakeNUp instead.
            // MakeNUp expects an array of source PDF file paths and a landscape flag.
            // For a single source PDF we pass a one‑element array. The method creates a 2×2 N‑up layout by default.
            bool success = editor.MakeNUp(new[] { inputPath }, outputPath, false);

            if (success)
                Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to create N‑up PDF.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

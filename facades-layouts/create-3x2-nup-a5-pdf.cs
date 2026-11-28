using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_3x2_A5.pdf";

        // Verify the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Create a 3×2 N‑up layout on an A5 sized page.
        // The correct method is MakeNUp, which takes an array of source PDFs and a landscape flag.
        // Since we have a single source PDF, we pass it in an array.
        // The method will arrange the pages in the required N‑up layout; the page size (A5) is the default for this overload.
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // false = portrait (A5), true = landscape if needed
        editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}

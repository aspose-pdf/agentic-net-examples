using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "documentA.pdf";
        const string outputPath = "nup_6x2_A5.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Create a 6‑by‑2 N‑up layout on A5 pages.
        // Overload: MakeNUp(string inputFile, string outputFile, int rows, int columns, PageSize pageSize)
        editor.MakeNUp(inputPath, outputPath, 6, 2, PageSize.A5);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
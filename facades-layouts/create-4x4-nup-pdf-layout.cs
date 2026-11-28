using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "nup_4x4.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap in a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Create a 4‑by‑4 N‑up layout (4 rows, 4 columns)
        // Signature: MakeNUp(string inputFile, string outputFile, int rows, int columns)
        editor.MakeNUp(inputPath, outputPath, 4, 4);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}
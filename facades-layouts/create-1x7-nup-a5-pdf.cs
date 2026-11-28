using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_1x7_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Aspose.Pdf.Facades.PdfFileEditor provides MakeNUp method.
        // MakeNUp takes an array of source PDF files, the output path and a flag indicating landscape orientation.
        // For a 1×7 N‑up layout we use a single source file and portrait orientation (A5 size).
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // portrait (A5)

        PdfFileEditor editor = new PdfFileEditor();
        editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}

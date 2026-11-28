using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "documentA.pdf";
        const string outputPath = "nup_2x6_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfFileEditor.MakeNUp (the correct method in the Facades API) to create an N‑up PDF.
        // The method accepts an array of source PDF file paths, the output file path, and a
        // flag indicating whether the resulting pages should be landscape.  The older NUp
        // method and NUpOptions class are not available in the referenced Aspose.Pdf version.
        PdfFileEditor editor = new PdfFileEditor();
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // A5 is portrait; set true only for landscape layout.
        editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}

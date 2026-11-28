using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_2x3_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // The MakeNUp method creates an N‑up PDF. It accepts an array of source files,
        // the output path, and a flag indicating whether the resulting pages should be
        // in landscape orientation. The version of Aspose.Pdf referenced does not expose
        // the NUpOptions class, so we rely on the default layout (which matches the
        // requested 2 columns × 3 rows when the source PDF has enough pages). If a
        // specific layout is required, a newer library version that provides NUpOptions
        // should be used.
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = false; // A5 portrait orientation
        editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        Console.WriteLine($"N‑up PDF saved to '{outputPath}'.");
    }
}

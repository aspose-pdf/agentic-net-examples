using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "documentA.pdf";
        const string outputPath = "nup_5x1_A5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Aspose.Pdf.Facades.PdfFileEditor does not expose NUpOptions or a direct NUp method in older versions.
        // Use the MakeNUp method which accepts an array of source PDFs, the output path, and a landscape flag.
        // For a 5×1 layout we want a landscape orientation (columns > rows).
        string[] sourceFiles = new[] { inputPath };
        bool isLandscape = true; // 5 columns horizontally => landscape

        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);

        if (success)
            Console.WriteLine($"N-up PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create N-up PDF.");
    }
}

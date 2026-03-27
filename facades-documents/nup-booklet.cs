using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string nupPath = "temp_nup.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Step 1: Create an N‑up layout (2 pages per sheet) using MakeNUp
        PdfFileEditor editor = new PdfFileEditor();
        // MakeNUp expects an array of source files, an output path and a landscape flag.
        // Passing a single source file creates a 2‑up layout; set isLandscape to false for portrait orientation.
        string[] sourceFiles = new[] { inputPath };
        bool nupSuccess = editor.MakeNUp(sourceFiles, nupPath, false);
        if (!nupSuccess)
        {
            Console.Error.WriteLine("N‑up operation failed.");
            return;
        }

        // Step 2: Convert the N‑up PDF into a booklet
        bool bookletSuccess = editor.MakeBooklet(nupPath, outputPath);
        if (!bookletSuccess)
        {
            Console.Error.WriteLine("Booklet generation failed.");
            return;
        }

        // Clean up the intermediate N‑up file
        try
        {
            File.Delete(nupPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"Booklet created successfully: {outputPath}");
    }
}

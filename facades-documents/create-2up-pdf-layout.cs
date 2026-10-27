using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // source PDF
        const string outputPath = "output_2up.pdf"; // result PDF

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so no using block is needed
        PdfFileEditor editor = new PdfFileEditor();

        // MakeNup expects an array of source files and a landscape flag.
        // For a simple 2‑up (two pages per sheet) we pass a single source file
        // and set isLandscape to false (portrait). The method will arrange pages
        // side‑by‑side automatically.
        bool success = editor.MakeNUp(new[] { inputPath }, outputPath, false);

        if (success)
            Console.WriteLine($"2‑up PDF saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create 2‑up PDF.");
    }
}
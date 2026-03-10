using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF path (N‑up result)
        const string outputPath = "nup_output.pdf";

        // Define grid layout: number of columns (x) and rows (y)
        const int columns = 2; // e.g., 2 columns
        const int rows    = 2; // e.g., 2 rows (creates a 2×2 N‑up)

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so no using block is needed.
        PdfFileEditor editor = new PdfFileEditor();

        // MakeNUp loads the input file, arranges pages in the specified grid,
        // and saves the result to the output file.
        bool success = editor.MakeNUp(inputPath, outputPath, columns, rows);

        if (success)
            Console.WriteLine($"N‑up PDF created successfully at '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to create N‑up PDF.");
    }
}
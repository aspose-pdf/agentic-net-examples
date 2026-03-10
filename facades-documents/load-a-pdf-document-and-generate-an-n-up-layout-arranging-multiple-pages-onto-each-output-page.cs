using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (N‑up result)
        const string outputPath = "output_nup.pdf";

        // Number of columns (x) and rows (y) per output page
        const int columns = 2; // e.g., 2 pages side‑by‑side horizontally
        const int rows = 2;    // e.g., 2 pages stacked vertically

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // Perform N‑up operation: arrange pages in a grid (columns x rows)
        // This method returns true on success, false otherwise.
        bool success = pdfEditor.MakeNUp(inputPath, outputPath, columns, rows);

        if (success)
        {
            Console.WriteLine($"N‑up PDF created successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
        }
    }
}
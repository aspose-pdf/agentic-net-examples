using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for PageSize enum if needed

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (N‑up result)
        const string outputPath = "nup_output.pdf";

        // Define the N‑up grid: number of columns (x) and rows (y)
        const int columns = 2; // e.g., 2 columns
        const int rows = 2;    // e.g., 2 rows

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create an instance of PdfFileEditor (no IDisposable, so no using block needed)
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the N‑up operation.
        // This overload creates a new PDF where each page contains a grid of the original pages.
        bool result = editor.MakeNUp(inputPath, outputPath, columns, rows);

        if (result)
        {
            Console.WriteLine($"N‑up PDF created successfully: {outputPath}");
        }
        else
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
        }
    }
}
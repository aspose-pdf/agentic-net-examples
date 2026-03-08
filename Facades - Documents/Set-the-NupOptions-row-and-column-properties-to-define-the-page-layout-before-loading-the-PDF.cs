using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting N‑up PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "nup_output.pdf";

        // Define the N‑up layout before processing the file
        // x = number of columns, y = number of rows
        int columns = 2; // columns (horizontal)
        int rows    = 3; // rows (vertical)

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create the PdfFileEditor facade (no IDisposable, so no using block needed)
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the N‑up operation; returns true on success
        bool result = editor.TryMakeNUp(inputPath, outputPath, columns, rows);

        if (result)
        {
            Console.WriteLine($"N‑up PDF created successfully at '{outputPath}' " +
                              $"with {columns} columns and {rows} rows.");
        }
        else
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
        }
    }
}
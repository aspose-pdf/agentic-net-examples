using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade API for N-Up operations

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath  = "input.pdf";
        // Output PDF file path (N-Up result)
        const string outputPath = "nup_output.pdf";

        // Define N-Up layout: number of columns (x) and rows (y)
        // These values are set BEFORE any PDF is loaded.
        int columns = 3; // x – number of columns
        int rows    = 2; // y – number of rows

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the PdfFileEditor facade (no IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Perform the N-Up operation.
            // TryMakeNUp returns true on success, false otherwise.
            bool success = editor.TryMakeNUp(inputPath, outputPath, columns, rows);

            if (success)
                Console.WriteLine($"N-Up PDF created successfully: {outputPath}");
            else
                Console.Error.WriteLine("N-Up operation failed.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., file access, format issues)
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
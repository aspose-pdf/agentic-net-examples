using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";

        // Intermediate N‑up PDF (2 columns × 2 rows)
        const string nupPath = "temp_nup.pdf";

        // Final booklet PDF
        const string outputPath = "booklet_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a PdfFileEditor instance (does not implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // Step 1: Create an N‑up layout.
            // This will place 2 columns and 2 rows of pages on each output page.
            // The overload used: MakeNUp(string inputFile, string outputFile, int x, int y)
            editor.MakeNUp(inputPath, nupPath, 2, 2);

            // Step 2: Convert the N‑up PDF into a booklet.
            // The overload used: MakeBooklet(string inputFile, string outputFile)
            editor.MakeBooklet(nupPath, outputPath);

            // Optional: clean up the intermediate N‑up file
            if (File.Exists(nupPath))
            {
                File.Delete(nupPath);
            }

            Console.WriteLine($"Booklet created successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
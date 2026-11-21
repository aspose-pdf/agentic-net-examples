using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Array of source PDF file paths to be processed
        string[] inputFiles = new string[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in a using block
        PdfFileEditor editor = new PdfFileEditor();

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build the output file name (e.g., doc1_3x2.pdf)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_3x2.pdf");

            try
            {
                // Create an N‑up PDF with 2 rows and 3 columns (total 6 pages per sheet)
                // PdfFileEditor does not have an NUp method – use MakeNUp instead.
                // MakeNUp accepts an array of source files, the output path, and a landscape flag.
                // Setting isLandscape to true yields a wider layout (3 columns).
                bool success = editor.MakeNUp(new[] { inputPath }, outputPath, true);

                if (success)
                {
                    Console.WriteLine($"N‑up PDF created: {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to create N‑up PDF for {inputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}

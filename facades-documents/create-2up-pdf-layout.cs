using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file (single source)
        const string inputPath = "input.pdf";

        // Output PDF file with 2‑up layout
        const string outputPath = "output_2up.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor provides the MakeNUp method for N‑up layout creation
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // MakeNUp expects an array of source PDF file paths.
            // For a single‑source 2‑up layout we pass the single file in the array.
            // The third argument indicates whether the resulting pages should be landscape.
            // The method returns a boolean indicating success.
            string[] sourceFiles = new[] { inputPath };
            bool isLandscape = false; // portrait orientation (2‑up will be side‑by‑side)

            bool success = editor.MakeNUp(sourceFiles, outputPath, isLandscape);
            if (success)
            {
                Console.WriteLine($"2‑up PDF created successfully: {outputPath}");
            }
            else
            {
                Console.Error.WriteLine("Failed to create 2‑up PDF: MakeNUp returned false.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create 2‑up PDF: {ex.Message}");
        }
    }
}

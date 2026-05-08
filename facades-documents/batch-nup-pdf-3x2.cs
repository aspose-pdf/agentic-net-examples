using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor resides in this namespace

class NUpBatchProcessor
{
    static void Main()
    {
        // List of input PDF file paths to be processed
        List<string> inputFiles = new List<string>
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
            // Add more file paths as needed
        };

        // Output directory for the N‑up PDFs
        string outputDir = "NUpOutput";
        Directory.CreateDirectory(outputDir);

        // Create a single PdfFileEditor instance (it does not implement IDisposable)
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // Process each PDF: create a 3‑column by 2‑row N‑up version
        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            // Build output file name (e.g., input1_nup.pdf)
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDir, $"{fileName}_nup.pdf");

            try
            {
                // MakeNUp(string inputFile, string outputFile, int columns, int rows)
                // columns = 3, rows = 2 as required
                bool success = pdfEditor.MakeNUp(inputPath, outputPath, 3, 2);

                if (success)
                {
                    Console.WriteLine($"N‑up created: {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to create N‑up for: {inputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        // No explicit disposal needed for PdfFileEditor
        Console.WriteLine("Batch processing completed.");
    }
}
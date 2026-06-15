using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for N‑up processing

class Program
{
    static void Main(string[] args)
    {
        // List of PDF files to be processed.
        // In a real scenario this could come from args, a config file, or a directory scan.
        string[] inputFiles = new string[]
        {
            "document1.pdf",
            "document2.pdf",
            "document3.pdf"
        };

        // Instantiate the PdfFileEditor once; it does NOT implement IDisposable.
        PdfFileEditor pfe = new PdfFileEditor();

        foreach (string inputPath in inputFiles)
        {
            // Verify that the source file exists before attempting processing.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            // Construct the output file name – same folder, original name with "_nup" suffix.
            string directory = Path.GetDirectoryName(inputPath);
            string baseName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{baseName}_nup.pdf");

            try
            {
                // Create an N‑up version: 3 columns × 2 rows = 6 pages per sheet.
                // Using the overload: MakeNUp(string inputFile, string outputFile, int x, int y)
                bool result = pfe.MakeNUp(inputPath, outputPath, 3, 2);

                if (result)
                {
                    Console.WriteLine($"Successfully created N‑up PDF: {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"MakeNUp returned false for: {inputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected errors but continue processing remaining files.
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
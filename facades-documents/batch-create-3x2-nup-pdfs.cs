using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where N‑up PDFs will be written
        const string outputFolder = "OutputNup";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        // PdfFileEditor provides the N‑up functionality
        PdfFileEditor pfe = new PdfFileEditor();

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name: original name + "_nup.pdf"
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_nup.pdf");

            try
            {
                // Create a 3‑column by 2‑row N‑up PDF (6 pages per sheet)
                bool result = pfe.MakeNUp(inputPath, outputPath, 3, 2);

                if (result)
                {
                    Console.WriteLine($"N‑up PDF created: {outputPath}");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to create N‑up PDF for: {inputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
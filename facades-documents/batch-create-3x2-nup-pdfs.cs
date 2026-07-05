using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputDir = "InputPdfs";
        // Directory where N‑up PDFs will be written
        const string outputDir = "OutputNup";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // Retrieve all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so a plain instance is sufficient
        PdfFileEditor pfe = new PdfFileEditor();

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Build output file name: original name + "_nup.pdf"
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDir, $"{fileNameWithoutExt}_nup.pdf");

                // Create a 3‑column by 2‑row N‑up version of the PDF
                // MakeNUp(string inputFile, string outputFile, int x, int y)
                bool result = pfe.MakeNUp(inputPath, outputPath, 3, 2);

                if (result)
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

        // No disposal required for PdfFileEditor
    }
}
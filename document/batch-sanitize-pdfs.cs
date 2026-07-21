using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to be sanitized
        const string inputFolder = "InputPdfs";

        // Output folder where cleaned PDFs will be saved
        const string outputFolder = "SanitizedPdfs";

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

        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Open the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Remove standard metadata (author, title, etc.)
                    doc.RemoveMetadata();

                    // Remove PDF/A and PDF/UA compliance flags if present
                    doc.RemovePdfaCompliance();
                    doc.RemovePdfUaCompliance();

                    // Optimize the document for web (linearize)
                    doc.Optimize();

                    // Save the sanitized copy to the output directory
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Sanitized: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch sanitization completed.");
    }
}
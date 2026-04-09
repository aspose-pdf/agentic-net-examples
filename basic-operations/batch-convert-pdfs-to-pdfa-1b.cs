using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Output folder where PDF/A‑1b files will be saved
        const string outputFolder = "OutputPdfA";

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

        foreach (string sourcePath in pdfFiles)
        {
            // Build output file name (same base name with PDF/A suffix)
            string baseName = Path.GetFileNameWithoutExtension(sourcePath);
            string destPath = Path.Combine(outputFolder, $"{baseName}_pdfa1b.pdf");
            // Optional log file for conversion messages
            string logPath = Path.Combine(outputFolder, $"{baseName}_conversion.log");

            try
            {
                // Load source PDF (Document implements IDisposable)
                using (Document doc = new Document(sourcePath))
                {
                    // Convert to PDF/A‑1b; errors are written to the log file
                    doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                    // Save the converted document; Save(string) always writes PDF
                    doc.Save(destPath);
                }

                Console.WriteLine($"Converted: {sourcePath} → {destPath}");
            }
            catch (Exception ex)
            {
                // Report any failure but continue processing remaining files
                Console.Error.WriteLine($"Error converting '{sourcePath}': {ex.Message}");
            }
        }
    }
}
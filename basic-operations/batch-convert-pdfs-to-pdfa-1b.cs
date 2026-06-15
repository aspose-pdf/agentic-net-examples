using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "InputPdfs";

        // Folder where PDF/A‑1b files will be written
        const string outputFolder = "OutputPdfA";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            // Build output file name (same base name with a suffix)
            string baseName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{baseName}_pdfa1b.pdf");

            try
            {
                // Load the source PDF
                using (Document doc = new Document(inputPath))
                {
                    // Convert to PDF/A‑1b; errors are written to a log file
                    string logPath = Path.Combine(outputFolder, $"{baseName}_convert.log");
                    doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                    // Save the converted PDF/A document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Converted: {inputPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to convert {inputPath}: {ex.Message}");
            }
        }
    }
}
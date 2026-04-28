using System;
using System.IO;
using Aspose.Pdf;

namespace PdfBatchConverter
{
    class Program
    {
        static void Main()
        {
            // Folder containing source PDF files
            const string inputFolder = "input_pdfs";
            // Folder where PDF/A‑1b files will be written
            const string outputFolder = "output_pdfa";

            if (!Directory.Exists(inputFolder))
            {
                Console.Error.WriteLine($"Input folder not found: {inputFolder}");
                return;
            }

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Get all PDF files in the input folder (non‑recursive)
            string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

            foreach (string pdfPath in pdfFiles)
            {
                // Build output file name (same base name with _pdfa suffix)
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(outputFolder, $"{baseName}_pdfa.pdf");
                // Optional log file for conversion errors
                string logPath = Path.Combine(outputFolder, $"{baseName}_log.txt");

                try
                {
                    // Load the source PDF
                    using (Document doc = new Document(pdfPath))
                    {
                        // Convert to PDF/A‑1b, logging any conversion errors
                        doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                        // Save the converted document (PDF/A‑1b)
                        doc.Save(outputPath);
                    }

                    Console.WriteLine($"Converted: {pdfPath} → {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }
        }
    }
}

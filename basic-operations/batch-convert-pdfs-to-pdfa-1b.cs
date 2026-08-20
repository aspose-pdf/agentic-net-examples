using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Output folder for PDF/A‑1b files
        const string outputFolder = @"C:\OutputPdfA";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Derive output file name (same name, PDF/A‑1b)
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + ".pdf");
            // Log file for conversion details
            string logPath = Path.Combine(outputFolder, fileName + "_conversion.log");

            try
            {
                // Load the source PDF
                using (Document doc = new Document(inputPath))
                {
                    // Convert to PDF/A‑1b, logging any conversion errors
                    doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                    // Save the converted document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Converted '{inputPath}' → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
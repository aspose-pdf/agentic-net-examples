using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to be processed
        const string inputFolder = "InputPdfs";
        // Output folder where PDF/A‑1b files will be saved
        const string outputFolder = "OutputPdfA";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, $"{baseName}_PDF_A_1b.pdf");
            string logPath = Path.Combine(outputFolder, $"{baseName}_conversion_log.xml");

            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Convert the document to PDF/A‑1b; log any conversion issues
                    doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                    // Save the converted document as a regular PDF (PDF/A is a PDF variant)
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
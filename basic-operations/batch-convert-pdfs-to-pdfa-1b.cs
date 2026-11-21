using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs
        const string inputFolder = "InputPdfs";
        // Output folder for PDF/A‑1b files
        const string outputFolder = "OutputPdfA";
        // Log file for conversion details
        const string logFilePath = "conversion_log.xml";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string sourcePath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourcePath);
            string destinationPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}_PDF_A_1b.pdf");

            try
            {
                // Load the source PDF
                using (Document doc = new Document(sourcePath))
                {
                    // Convert to PDF/A‑1b
                    doc.Convert(logFilePath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                    // Save the converted PDF
                    doc.Save(destinationPath);
                }

                Console.WriteLine($"Converted: {sourcePath} → {destinationPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting '{sourcePath}': {ex.Message}");
            }
        }
    }
}
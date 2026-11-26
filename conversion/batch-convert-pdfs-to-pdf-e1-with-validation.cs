using System;
using System.IO;
using Aspose.Pdf;

class BatchPdfEConverter
{
    static void Main()
    {
        // Determine the input folder. If the configured folder does not exist, fall back to the current directory.
        string inputFolder = @"C:\Batch\Input";
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}. Using current working directory instead.");
            inputFolder = Directory.GetCurrentDirectory();
        }

        // Folder where converted PDFs and reports will be saved (ensure it exists).
        string outputFolder = @"C:\Batch\Output";
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the source folder.
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Process finished.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Derive base file name without extension.
                string baseName = Path.GetFileNameWithoutExtension(inputPath);

                // Paths for the converted PDF/E‑1 file and its validation report.
                string outputPdfPath = Path.Combine(outputFolder, $"{baseName}_PDFE1.pdf");
                string reportPath    = Path.Combine(outputFolder, $"{baseName}_PDFE1_report.xml");

                // Load the source PDF inside a using block for deterministic disposal.
                using (Document doc = new Document(inputPath))
                {
                    // Convert the document to PDF/E‑1. The conversion log is optional – we keep it for completeness.
                    string conversionLog = Path.Combine(outputFolder, $"{baseName}_conversion_log.xml");
                    doc.Convert(conversionLog, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                    // Save the converted PDF/E‑1 document.
                    doc.Save(outputPdfPath);
                }

                // Validate the generated PDF/E‑1 file. The Validate method writes an XML report
                // and returns a Boolean indicating whether the document conforms to PDF/E‑1.
                bool isValid;
                using (Document valDoc = new Document(outputPdfPath))
                {
                    isValid = valDoc.Validate(reportPath, PdfFormat.PDF_E_1);
                }

                // Write a short human‑readable summary (still keeping the XML report).
                string summaryPath = Path.Combine(outputFolder, $"{baseName}_PDFE1_summary.txt");
                using (StreamWriter writer = new StreamWriter(summaryPath, false))
                {
                    writer.WriteLine($"Validation Summary for: {Path.GetFileName(outputPdfPath)}");
                    writer.WriteLine($"Generated on: {DateTime.Now}");
                    writer.WriteLine($"Is Valid (PDF/E‑1): {isValid}");
                }

                Console.WriteLine($"Processed: {baseName}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors for the current file.
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion and validation completed.");
    }
}

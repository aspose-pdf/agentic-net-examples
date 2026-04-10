using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class PdfSanitizer
{
    static void Main()
    {
        // Input folder containing PDFs to be sanitized
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where sanitized PDFs will be saved
        const string outputFolder = @"C:\SanitizedPdfs";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Create output folder if it does not exist
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
                {
                    // Remove metadata (author, title, etc.)
                    doc.RemoveMetadata();

                    // Remove PDF/A compliance if present
                    doc.RemovePdfaCompliance();

                    // Remove PDF/UA compliance if present
                    doc.RemovePdfUaCompliance();

                    // Delete all embedded file attachments, if any
                    if (doc.EmbeddedFiles != null)
                    {
                        doc.EmbeddedFiles.Delete();
                    }

                    // Build the output file path (preserve original file name)
                    string fileName = Path.GetFileName(inputPath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the sanitized PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Sanitized: {Path.GetFileName(inputPath)} → {outputFolder}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch sanitization completed.");
    }
}
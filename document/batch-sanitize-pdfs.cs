using System;
using System.IO;
using Aspose.Pdf;

class PdfSanitizer
{
    static void Main()
    {
        // Input folder containing PDFs to sanitize
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where sanitized PDFs will be saved
        const string outputFolder = @"C:\SanitizedPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Remove document metadata (author, title, etc.)
                    doc.RemoveMetadata();

                    // Remove PDF/A compliance if present
                    doc.RemovePdfaCompliance();

                    // Remove PDF/UA compliance if present
                    doc.RemovePdfUaCompliance();

                    // Flatten form fields and annotations (optional but often part of sanitization)
                    doc.Flatten();

                    // Optimize resources: remove unused objects and merge duplicates
                    doc.OptimizeResources();

                    // Optional: validate the document after modifications
                    // The boolean parameter indicates whether to perform a deep check
                    doc.Check(true);

                    // Build the output file path preserving the original file name
                    string fileName = Path.GetFileName(inputPath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the sanitized PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Sanitized: {Path.GetFileName(inputPath)}");
            }
            catch (PdfException ex) // Aspose.Pdf specific exception type
            {
                // Specific exception for PDF processing failures
                Console.Error.WriteLine($"Sanitization failed for '{inputPath}': {ex.Message}");
            }
            catch (Exception ex)
            {
                // General exception handling
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch sanitization completed.");
    }
}

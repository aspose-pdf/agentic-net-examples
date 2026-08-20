using System;
using System.IO;
using Aspose.Pdf;

class PdfSanitizer
{
    static void Main()
    {
        // Input folder containing PDFs to be sanitized
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where cleaned PDFs will be saved
        const string outputFolder = @"C:\SanitizedPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string sourcePath in pdfFiles)
        {
            // Determine the destination path with the same file name
            string fileName = Path.GetFileName(sourcePath);
            string destPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(sourcePath))
                {
                    // ---- Sanitization steps ----

                    // Remove all document metadata (author, title, etc.)
                    doc.RemoveMetadata();

                    // Remove PDF/UA compliance information (if present)
                    doc.RemovePdfUaCompliance();

                    // Remove PDF/A compliance information (if present)
                    doc.RemovePdfaCompliance();

                    // Flatten form fields and annotations so only their visual appearance remains
                    doc.Flatten();

                    // Optimize resources: remove unused objects, merge duplicates, etc.
                    doc.OptimizeResources();

                    // Save the cleaned PDF to the target folder
                    doc.Save(destPath);
                }

                Console.WriteLine($"Sanitized: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("All PDFs have been processed.");
    }
}
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

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build the output file path (same file name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Load the PDF document (lifecycle rule: use constructor)
                using (Document doc = new Document(inputPath))
                {
                    // ---- Sanitization steps ----
                    // Remove all document metadata
                    doc.RemoveMetadata();

                    // Remove PDF/A compliance information if present
                    doc.RemovePdfaCompliance();

                    // Remove PDF/UA compliance information if present
                    doc.RemovePdfUaCompliance();

                    // Flatten form fields and annotations into the page content
                    doc.Flatten();

                    // Optimize resources (remove unused objects, merge duplicates, etc.)
                    doc.OptimizeResources();

                    // Save the sanitized PDF (save without explicit SaveOptions writes PDF)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Sanitized: {Path.GetFileName(inputPath)} → {outputPath}");
            }
            catch (Exception ex)
            {
                // General error handling (covers any sanitization‑related failures)
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch sanitization completed.");
    }
}

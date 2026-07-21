using System;
using System.IO;
using Aspose.Pdf;

class PdfSanitizer
{
    static void Main()
    {
        // Input folder containing PDFs to be sanitized
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where sanitized PDFs will be saved
        const string outputFolder = @"C:\SanitizedPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string inputPath in pdfFiles)
        {
            // Determine the output file path (same file name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Load, sanitize, and save each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Remove document metadata (author, title, etc.)
                doc.RemoveMetadata();

                // Remove PDF/A compliance if present
                doc.RemovePdfaCompliance();

                // Remove PDF/UA compliance if present
                doc.RemovePdfUaCompliance();

                // Flatten form fields and annotations (places values directly on the page)
                doc.Flatten();

                // Optimize resources (remove unused objects, merge duplicates)
                doc.OptimizeResources();

                // Save the sanitized PDF to the target folder
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized: {Path.GetFileName(inputPath)} → {outputPath}");
        }

        Console.WriteLine("All PDFs have been sanitized.");
    }
}
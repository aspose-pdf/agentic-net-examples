using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where sanitized PDFs will be saved
        const string outputFolder = "SanitizedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string inputPath in pdfFiles)
        {
            string fileName   = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: use using for deterministic disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Remove document metadata
                    doc.RemoveMetadata();

                    // Remove PDF/A and PDF/UA compliance flags, if present
                    doc.RemovePdfaCompliance();
                    doc.RemovePdfUaCompliance();

                    // Optimize resources: remove unused objects and merge duplicates
                    doc.OptimizeResources();

                    // Flatten form fields and annotations into the page content
                    doc.Flatten();

                    // Save the sanitized PDF (lifecycle rule: use Document.Save(string))
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Sanitized: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process {fileName}: {ex.Message}");
            }
        }
    }
}
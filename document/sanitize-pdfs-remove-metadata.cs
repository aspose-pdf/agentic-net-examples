using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to be sanitized
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where cleaned PDFs will be saved
        const string outputFolder = @"C:\SanitizedPdfs";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // ---- Sanitization steps ----
                // Remove all metadata
                doc.RemoveMetadata();

                // Remove PDF/UA compliance (accessibility tags)
                doc.RemovePdfUaCompliance();

                // Remove PDF/A compliance (archival tags)
                doc.RemovePdfaCompliance();

                // Flatten form fields (replace fields with their appearances)
                doc.Flatten();

                // Optimize resources (remove unused objects, merge duplicates)
                doc.OptimizeResources();

                // Optional: validate the document after sanitization
                // bool isValid = doc.Check(true);

                // Build the output file path (append "_clean" to the original name)
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string cleanedPath = Path.Combine(outputFolder, fileName + "_clean.pdf");

                // Save the sanitized PDF
                doc.Save(cleanedPath);
            }

            Console.WriteLine($"Sanitized PDF saved: {pdfPath}");
        }

        Console.WriteLine("All PDFs have been processed.");
    }
}
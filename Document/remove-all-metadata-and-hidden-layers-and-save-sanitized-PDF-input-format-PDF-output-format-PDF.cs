using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Remove all document metadata
                doc.RemoveMetadata();

                // Remove PDF/A and PDF/UA compliance (if present)
                doc.RemovePdfaCompliance();
                doc.RemovePdfUaCompliance();

                // Optimize resources – this removes unused objects,
                // including hidden optional content groups (layers)
                doc.OptimizeResources();

                // Save the sanitized PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
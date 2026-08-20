using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned.pdf";

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
                // Remove hidden data
                doc.RemoveMetadata();          // strips document metadata
                doc.RemovePdfaCompliance();    // removes PDF/A compliance flags
                doc.RemovePdfUaCompliance();   // removes PDF/UA compliance flags

                // Optimize resources to discard unused objects
                doc.OptimizeResources();

                // Re‑process paragraphs to rebuild structure (headings) after cleaning
                doc.ProcessParagraphs();

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
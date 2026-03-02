using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Remove all document metadata (author, title, etc.)
                doc.RemoveMetadata();

                // Remove PDF/A compliance information if present
                doc.RemovePdfaCompliance();

                // Remove PDF/UA (accessibility) compliance information if present
                doc.RemovePdfUaCompliance();

                // Ensure signature fields are sanitized (enabled by default)
                doc.EnableSignatureSanitization = true;

                // Optional: optimize resources to drop unused objects
                doc.OptimizeResources();

                // Save the sanitized PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during sanitization: {ex.Message}");
        }
    }
}
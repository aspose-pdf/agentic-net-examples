using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Remove all standard metadata (title, author, subject, etc.)
                doc.RemoveMetadata();

                // Remove PDF/A and PDF/UA compliance flags if present
                doc.RemovePdfaCompliance();
                doc.RemovePdfUaCompliance();

                // Flatten transparent content to eliminate hidden layers
                doc.FlattenTransparency();

                // Optimize resources: delete unused objects and merge duplicates
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
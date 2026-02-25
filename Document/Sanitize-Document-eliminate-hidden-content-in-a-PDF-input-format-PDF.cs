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
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Remove document metadata (author, title, custom metadata, etc.)
                doc.RemoveMetadata();

                // Remove PDF/A and PDF/UA compliance flags that may hide content
                doc.RemovePdfaCompliance();
                doc.RemovePdfUaCompliance();

                // Flatten form fields and annotations so their appearances become part of the page content
                doc.Flatten();

                // Replace any transparent objects with opaque raster/vector equivalents
                doc.FlattenTransparency();

                // Optimize resources: remove unused objects and merge duplicates
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
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove all document metadata (author, title, custom entries, etc.)
            doc.RemoveMetadata();

            // Remove PDF/A compliance information if present
            doc.RemovePdfaCompliance();

            // Remove PDF/UA (accessibility) compliance information if present
            doc.RemovePdfUaCompliance();

            // Flatten form fields and annotations so only their visual appearance remains
            doc.Flatten();

            // Flatten any transparent content to non‑transparent raster/vector graphics
            doc.FlattenTransparency();

            // Optimize resources: remove unused objects and merge duplicates
            doc.OptimizeResources();

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
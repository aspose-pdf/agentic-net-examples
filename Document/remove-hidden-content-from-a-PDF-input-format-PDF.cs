using System;
using System.IO;
using Aspose.Pdf;

class RemoveHiddenContent
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_clean.pdf";

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
                // Remove document metadata (title, author, etc.)
                doc.RemoveMetadata();

                // Remove PDF/A and PDF/UA compliance information if present
                doc.RemovePdfaCompliance();
                doc.RemovePdfUaCompliance();

                // Flatten transparent content to raster graphics
                doc.FlattenTransparency();

                // Optimize resources: delete unused objects and merge duplicates
                doc.OptimizeResources();

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Hidden content removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
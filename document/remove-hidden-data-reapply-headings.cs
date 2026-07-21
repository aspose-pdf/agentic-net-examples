using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

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
                // ----- Remove hidden data -----
                // Remove standard metadata entries
                doc.RemoveMetadata();

                // Remove PDF/A and PDF/UA compliance flags
                doc.RemovePdfaCompliance();
                doc.RemovePdfUaCompliance();

                // Delete outline/bookmark entries (optional hidden navigation)
                doc.Outlines.Delete();

                // Flatten form fields and annotations into static content
                doc.Flatten();

                // Optimize resources to discard unused objects
                doc.OptimizeResources();

                // ----- Re‑apply headings for navigation -----
                // Enable automatic tagging which detects headings and builds a structure tree
                AutoTaggingSettings.Default.EnableAutoTagging = true;
                // Optional: configure heading detection strategy
                // AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

                // Save the cleaned and re‑tagged PDF
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
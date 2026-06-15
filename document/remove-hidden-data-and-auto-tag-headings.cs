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
        const string outputPath = "cleaned_tagged.pdf";

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
                doc.RemoveMetadata();          // delete document metadata
                doc.RemovePdfaCompliance();    // clear PDF/A compliance flags
                doc.RemovePdfUaCompliance();   // clear PDF/UA compliance flags
                doc.OptimizeResources();       // purge unused resources

                // Re‑apply headings to preserve navigation after cleaning
                AutoTaggingSettings.Default.EnableAutoTagging = true;
                // Optional: improve heading detection
                // AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

                // Save the cleaned, tagged PDF
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
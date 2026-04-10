using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // 1. Remove hidden or private data
            doc.RemoveMetadata();          // strips document metadata
            doc.RemovePdfaCompliance();    // removes PDF/A compliance flags
            doc.RemovePdfUaCompliance();   // removes PDF/UA compliance flags
            doc.Flatten();                 // flattens form fields and annotations
            doc.OptimizeResources();       // removes unused resources

            // 2. Re‑apply heading detection to preserve navigation
            // AutoTaggingSettings is a global static; enable it before saving.
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            // Optional: let the engine auto‑detect heading levels
            AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

            // 3. Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
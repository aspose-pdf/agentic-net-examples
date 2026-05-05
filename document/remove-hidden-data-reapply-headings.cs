using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Remove hidden data and compliance information
            // -------------------------------------------------
            doc.RemoveMetadata();          // strips document metadata
            doc.RemovePdfaCompliance();    // removes PDF/A compliance flags
            doc.RemovePdfUaCompliance();   // removes PDF/UA compliance flags
            doc.OptimizeResources();       // drops unused resources (fonts, images, etc.)
            doc.Flatten();                 // flattens form fields and annotations

            // -------------------------------------------------
            // 2. Re‑apply headings to preserve navigation
            // -------------------------------------------------
            // Enable automatic tagging (headings detection) – static global setting
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Process paragraphs to generate heading structure elements
            // This creates a logical structure tree with headings based on detected text styles
            doc.ProcessParagraphs();

            // -------------------------------------------------
            // 3. Save the cleaned and re‑tagged PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
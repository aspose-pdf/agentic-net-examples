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

        // Enable auto‑tagging so headings are detected after cleaning
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        using (Document doc = new Document(inputPath))
        {
            // Remove hidden data
            doc.RemoveMetadata();          // delete document metadata
            doc.RemovePdfaCompliance();    // remove PDF/A compliance
            doc.RemovePdfUaCompliance();   // remove PDF/UA compliance
            doc.OptimizeResources();       // drop unused resources

            // Set language and title for the tagged PDF
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Save the cleaned, re‑tagged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
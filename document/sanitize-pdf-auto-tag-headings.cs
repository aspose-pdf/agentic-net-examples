using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged; // contains AutoTaggingSettings, HeadingRecognitionStrategy

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "clean_navigable.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, sanitize it, apply auto‑tagging for headings, and save.
        using (Document doc = new Document(inputPath))
        {
            // ---- Sanitization ----
            doc.RemoveMetadata();          // Remove all document metadata.
            doc.RemovePdfUaCompliance();   // Remove PDF/UA compliance flags.
            doc.RemovePdfaCompliance();    // Remove PDF/A compliance flags.

            // ---- Auto‑tagging (heading creation) ----
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;
            // Optional: customize heading levels if needed.
            // AutoTaggingSettings.Default.HeadingLevels = new HeadingLevels();

            // Ensure tagged content is initialized and set language/title.
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Save the cleaned, navigable PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}

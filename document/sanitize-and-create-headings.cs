using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "clean_navigable.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ---------- Sanitization ----------
            // Remove all metadata entries
            doc.RemoveMetadata();
            // Flatten form fields and annotations into static content
            doc.Flatten();

            // ---------- Heading creation (auto‑tagging) ----------
            // Enable automatic tagging which also detects headings based on font size
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            // Process paragraphs to generate the logical structure and headings
            doc.ProcessParagraphs();

            // Set language and title for the tagged PDF
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Save the cleaned, navigable PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF with headings saved to '{outputPath}'.");
    }
}

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
        const string outputPath = "output_with_headings.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Enable automatic tagging and configure heading detection
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;
            // Optional: customize heading levels if needed
            // AutoTaggingSettings.Default.HeadingLevels = new HeadingLevels();

            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Example: add a top‑level heading (H1)
            HeaderElement h1 = taggedContent.CreateHeaderElement(1);
            h1.SetText("Document Title");
            root.AppendChild(h1);

            // Example: add a second‑level heading (H2) under the first heading
            HeaderElement h2 = taggedContent.CreateHeaderElement(2);
            h2.SetText("Section 1 – Introduction");
            root.AppendChild(h2);

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with headings saved to '{outputPath}'.");
    }
}
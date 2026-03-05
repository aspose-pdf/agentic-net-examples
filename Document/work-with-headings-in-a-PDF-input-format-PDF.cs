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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Configure global auto‑tagging settings (optional but shown for completeness)
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;
        AutoTaggingSettings.Default.HeadingLevels = new HeadingLevels(); // default mapping

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set document‑level accessibility metadata
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with Added Headings");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a level‑1 heading and add it to the structure
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Chapter 1 – Introduction");
            root.AppendChild(heading); // AppendChild with one argument (bool defaults)

            // Create a paragraph under the heading
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph is associated with the newly added heading.");
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
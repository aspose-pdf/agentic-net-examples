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

        // Enable auto‑tagging (sanitization) globally
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        // Use heuristic heading detection (optional, can be Auto or Outlines)
        AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Heuristic;

        // Load the source PDF (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a top‑level heading (H1) and add it to the root
            HeaderElement h1 = tagged.CreateHeaderElement(1);
            h1.SetText("Document Title");
            root.AppendChild(h1);

            // Create a second‑level heading (H2)
            HeaderElement h2 = tagged.CreateHeaderElement(2);
            h2.SetText("Section 1");
            root.AppendChild(h2);

            // Add a paragraph under the second heading
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This is the first paragraph of Section 1.");
            root.AppendChild(para);

            // Save the cleaned, navigable PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Clean, navigable PDF saved to '{outputPath}'.");
    }
}
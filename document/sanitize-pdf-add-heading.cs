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

        // Enable global auto‑tagging (sanitization) and set a heuristic heading strategy
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Heuristic;

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set language and title via the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Obtain the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a top‑level heading (H1) to serve as a navigation entry
            HeaderElement h1 = tagged.CreateHeaderElement(1);
            h1.SetText("Document Overview");
            h1.Language = "en-US";               // optional language attribute
            root.AppendChild(h1);                 // attach heading to the root

            // Add a descriptive paragraph beneath the heading
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This PDF has been sanitized and structured for better navigation.");
            root.AppendChild(para);               // attach paragraph to the root

            // Save the PDF; auto‑tagging is applied automatically on save
            doc.Save(outputPath);
        }

        Console.WriteLine($"Clean, navigable PDF saved to '{outputPath}'.");
    }
}
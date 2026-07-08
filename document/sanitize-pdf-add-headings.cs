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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Enable auto‑tagging globally and use automatic heading detection
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ----- Sanitization -----
            doc.RemoveMetadata();   // strip metadata
            doc.Flatten();          // flatten form fields
            doc.OptimizeResources(); // remove unused resources
            doc.Check(true);        // validate the document

            // ----- Tagging / Heading creation -----
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a top‑level heading (H1)
            HeaderElement h1 = tagged.CreateHeaderElement(1);
            h1.SetText("Document Title");
            root.AppendChild(h1);

            // Add a paragraph under the heading
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This PDF has been sanitized and tagged for better navigation.");
            root.AppendChild(para);

            // Save the cleaned, navigable PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Enable auto‑tagging (sanitization) and configure heading detection
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a top‑level heading (H1) for the document title
            HeaderElement h1 = tagged.CreateHeaderElement(1);
            h1.SetText("Document Title");
            root.AppendChild(h1);

            // Create a second‑level heading (H2) for a section
            HeaderElement h2 = tagged.CreateHeaderElement(2);
            h2.SetText("Section 1");
            root.AppendChild(h2);

            // Add a paragraph under the second heading
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This is the first paragraph of Section 1.");
            root.AppendChild(para);

            // Save the cleaned, navigable PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Clean, navigable PDF saved to '{outputPath}'.");
    }
}
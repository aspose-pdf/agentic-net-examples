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
        const string outputPath = "clean_navigable.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Enable auto‑tagging (sanitization) and configure heading detection
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a top‑level heading (e.g., H1) for the document title
            HeaderElement titleHeader = tagged.CreateHeaderElement(1);
            titleHeader.SetText("Document Title");
            root.AppendChild(titleHeader);

            // Add a paragraph under the heading
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This PDF has been sanitized and structured for better navigation.");
            root.AppendChild(para);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Clean, navigable PDF saved to '{outputPath}'.");
    }
}
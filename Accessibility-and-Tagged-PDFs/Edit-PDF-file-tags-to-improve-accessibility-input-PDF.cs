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
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Enable automatic tagging for accessibility
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Obtain the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title metadata for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Add a paragraph element to the structure tree
            StructureElement root = tagged.RootElement;
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This document has been processed to improve accessibility.");
            root.AppendChild(paragraph); // AppendChild with a single argument

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessibility‑enhanced PDF saved to '{outputPath}'.");
    }
}
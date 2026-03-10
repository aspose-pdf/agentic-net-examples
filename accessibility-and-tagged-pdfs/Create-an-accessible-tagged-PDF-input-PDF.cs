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
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Enable global auto‑tagging (optional but recommended for accessibility)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the PDF (accessibility metadata)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Obtain the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a new paragraph element and add it to the structure tree
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This PDF has been made accessible using Aspose.Pdf.");
            root.AppendChild(paragraph); // AppendChild with a single argument

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
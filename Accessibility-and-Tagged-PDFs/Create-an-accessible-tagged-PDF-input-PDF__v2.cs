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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Enable automatic tagging globally (optional but recommended)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for accessibility metadata
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Obtain the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a paragraph element and assign its text
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This PDF has been made accessible using Aspose.Pdf.");

            // Append the paragraph to the root of the structure tree
            root.AppendChild(paragraph);

            // Save the modified PDF with the new tagged structure
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
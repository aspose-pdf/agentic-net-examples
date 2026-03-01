using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "accessible_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language and title for the tagged PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a paragraph element, set its text, and attach it to the root
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();
            paragraph.SetText("This PDF has been made accessible using Aspose.Pdf.");
            root.AppendChild(paragraph); // AppendChild with a single argument

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}
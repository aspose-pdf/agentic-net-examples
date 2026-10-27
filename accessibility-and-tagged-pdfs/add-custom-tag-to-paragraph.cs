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
        const string outputPath = "custom_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF and obtain the tagged content interface
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language and title for the tagged PDF (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a new paragraph element via the factory
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();

            // Assign a custom tag name to represent a specialized content type
            // Use SetTag method because Tag is a method group, not a property
            paragraph.SetTag("MyCustomTag");

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph uses a custom tag name for specialized content.");

            // Attach the paragraph to the document's structure tree
            root.AppendChild(paragraph);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with custom tagged paragraph saved to '{outputPath}'.");
    }
}

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
        const string outputPath = "output_custom_tag.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a new paragraph element via the factory
            ParagraphElement para = tagged.CreateParagraphElement();

            // Assign a custom tag name to represent specialized content
            para.SetTag("MySpecialParagraph");

            // Set the visible text of the paragraph
            para.SetText("This paragraph uses a custom tag.");

            // Attach the paragraph to the root element
            root.AppendChild(para);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom tag to '{outputPath}'.");
    }
}

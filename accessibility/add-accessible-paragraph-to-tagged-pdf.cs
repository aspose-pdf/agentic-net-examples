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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and keep its existing tag structure
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optionally set language or title (preserves existing tags)
            taggedContent.SetLanguage("en-US");

            // Get the root of the logical structure (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Create a new paragraph element, set its text, and add it to the root
            ParagraphElement newParagraph = taggedContent.CreateParagraphElement();
            newParagraph.SetText("This is a newly added accessible paragraph.");
            root.AppendChild(newParagraph); // AppendChild with single argument

            // Save the PDF; existing tags are preserved and the new paragraph is added
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input_tagged.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (must be tagged)
        using (Document doc = new Document(inputPath))
        {
            // Verify the document is actually tagged – the IsTagged property does not exist.
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The PDF is not tagged. No tagging operations will be performed.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a new section (DivElement) under the root
            DivElement section = taggedContent.CreateDivElement();
            section.AlternativeText = "Custom section added by code";
            root.AppendChild(section); // attach section to the tree

            // Create a paragraph element with a custom tag
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();
            paragraph.SetText("This is a newly added paragraph with a custom tag.");
            // "Tag" is a method, not a property – use SetTag to assign a custom tag identifier
            paragraph.SetTag("MyCustomTag");
            paragraph.AlternativeText = "Paragraph describing custom content";

            // Attach the paragraph to the newly created section
            section.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}

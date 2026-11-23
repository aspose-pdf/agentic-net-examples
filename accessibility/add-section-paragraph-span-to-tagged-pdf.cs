using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_tagged.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing tagged PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optional: set language and title for the document
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Create a new section (using DivElement as a generic container)
            DivElement section = taggedContent.CreateDivElement();
            section.AlternativeText = "New section added by code";

            // Create a paragraph element
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();

            // Create a span element and set its text
            SpanElement span = taggedContent.CreateSpanElement();
            span.SetText("This is a span inside a paragraph within a new section.");

            // Build the hierarchy: Span -> Paragraph -> Section -> Root
            paragraph.AppendChild(span);   // add span to paragraph
            section.AppendChild(paragraph); // add paragraph to section
            root.AppendChild(section);      // attach section to the document root

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
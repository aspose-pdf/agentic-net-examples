using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement, SpanElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "ParagraphTagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a paragraph element via the ITaggedContent factory
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();

            // Create a span element (inline text) and set its text
            SpanElement span = taggedContent.CreateSpanElement();
            span.SetText("This is a span inside a paragraph.");

            // Attach the span to the paragraph
            paragraph.AppendChild(span);   // AppendChild with single argument

            // Attach the paragraph to the root of the structure tree
            root.AppendChild(paragraph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved as '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement, SpanElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input_tagged.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document is tagged; otherwise the API will still work but may create a new structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a new paragraph element
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();

            // Create a span element and set its text
            SpanElement span = taggedContent.CreateSpanElement();
            span.SetText("This is a newly inserted span inside a paragraph.");

            // Append the span to the paragraph
            paragraph.AppendChild(span);   // AppendChild with one argument (bool defaults to true)

            // Append the paragraph to the root of the structure tree
            root.AppendChild(paragraph);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
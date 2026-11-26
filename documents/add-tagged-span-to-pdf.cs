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
        const string outputPath = "ModifiedTag.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document is marked as tagged (Aspose.Pdf will create the structure if missing)
            // Set language and title for accessibility (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a Span element, set its text, and attach it to the root.
            // The Span element will be emitted as a BDC/EMC pair in the PDF content stream.
            SpanElement span = taggedContent.CreateSpanElement();
            span.SetText("This is a newly added span for accessibility.");
            // Optionally set alternative text for the span
            span.AlternativeText = "Accessible span text";

            // Append the span to the root element
            root.AppendChild(span);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
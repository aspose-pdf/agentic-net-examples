using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "heading_language.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Enable tagged PDF and set document language
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Heading");

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a heading (H1) element, set its text and language attribute
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Sample Heading");
            heading.Language = "en-US"; // language attribute on the heading element

            // Attach the heading to the structure tree
            root.AppendChild(heading);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}
using System;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "heading.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Heading");

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a Level 1 heading element
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Level 1 Heading");

            // Attach the heading to the document structure
            root.AppendChild(heading);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
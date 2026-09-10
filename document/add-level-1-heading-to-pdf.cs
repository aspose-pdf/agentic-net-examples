using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "heading.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root structure element (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Create a Level 1 heading element
            HeaderElement heading = taggedContent.CreateHeaderElement(1);
            heading.SetText("Level 1 Heading");

            // Attach the heading to the document's structure tree
            root.AppendChild(heading);

            // Save the PDF file (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
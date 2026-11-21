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

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");                     // Set document language
            tagged.SetTitle("Document with Heading");        // Set document title

            // Get the root of the structure tree (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a Level 1 heading element and set its text
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Level 1 Heading");

            // Attach the heading to the root element
            root.AppendChild(heading);

            // Save the PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
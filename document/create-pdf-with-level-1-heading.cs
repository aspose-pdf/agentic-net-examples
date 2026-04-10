using System;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "heading.pdf";

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optionally set language and title for the document
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Heading");

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a Level 1 heading element
            HeaderElement h1 = tagged.CreateHeaderElement(1);
            h1.SetText("Level 1 Heading");

            // Attach the heading to the document structure
            root.AppendChild(h1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
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

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            // Set language (optional, improves accessibility)
            tagged.SetLanguage("en-US");
            // Set a title for the document
            tagged.SetTitle("Document with Heading");

            // Get the root of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a Level 1 heading element
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Level 1 Heading");

            // Attach the heading to the root element
            root.AppendChild(heading);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
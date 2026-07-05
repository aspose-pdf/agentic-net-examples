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

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document‑level language and title
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Heading");

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a level‑1 heading element
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Sample Heading");

            // Assign a language attribute to the heading element
            heading.Language = "en-US";

            // Attach the heading to the document's structure tree
            root.AppendChild(heading);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
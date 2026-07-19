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
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document‑level language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Heading");

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a level‑1 heading element
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Chapter 1: Introduction");

            // Assign a language attribute to the heading element
            heading.Language = "en-US";

            // Attach the heading to the root of the structure tree
            root.AppendChild(heading); // bool parameter has a default value

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "heading_document.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set the document language (applies to all content unless overridden)
            tagged.SetLanguage("en-US");
            // Optional: set a title for the PDF
            tagged.SetTitle("Document with Heading");

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a heading element (level 1)
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Sample Heading");
            // Assign a language attribute specifically to this heading
            heading.Language = "en-US";

            // Attach the heading to the root element
            root.AppendChild(heading);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created and saved to '{outputPath}'.");
    }
}
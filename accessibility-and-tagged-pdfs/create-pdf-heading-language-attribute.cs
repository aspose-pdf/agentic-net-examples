using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "heading_with_language.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set the document language (applies to all content unless overridden)
            taggedContent.SetLanguage("en-US");

            // Optional: set a title for the PDF document
            taggedContent.SetTitle("Document with Heading and Language Attribute");

            // Get the root element of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a heading (HeaderElement) at level 1
            HeaderElement heading = taggedContent.CreateHeaderElement(1);
            heading.SetText("Sample Heading");
            // Assign a language attribute specifically to this heading element
            heading.Language = "en-US";

            // Append the heading to the root element
            root.AppendChild(heading);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
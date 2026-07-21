using System;
using System.IO;
using Aspose.Pdf;                         // Document, Color, etc.
using Aspose.Pdf.Tagged;                 // ITaggedContent
using Aspose.Pdf.LogicalStructure;       // StructureElement, ParagraphElement

class Program
{
    static void Main()
    {
        const string outputPath = "custom_tag_paragraph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Custom Tagged Paragraph");

            // Get the root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element via the factory
            ParagraphElement para = tagged.CreateParagraphElement();

            // Assign a custom tag name to represent a specialized content type
            para.SetTag("MySpecialParagraph");

            // Set the visible text of the paragraph
            para.SetText("This paragraph carries a custom tag for specialized processing.");

            // Append the paragraph to the root of the structure tree
            root.AppendChild(para);   // AppendChild with a single argument (bool defaults)

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
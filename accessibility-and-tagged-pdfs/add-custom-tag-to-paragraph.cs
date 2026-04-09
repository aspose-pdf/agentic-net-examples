using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "custom_tag_paragraph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Custom Tagged Paragraph");

            // Get the root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Assign a custom tag name to represent specialized content
            paragraph.SetTag("MySpecialParagraph");

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph uses a custom tag for specialized content.");

            // Append the paragraph to the root element
            root.AppendChild(paragraph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
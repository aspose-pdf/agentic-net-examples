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
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set document language and title (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle("Document with Custom Tag Paragraph");

            // Get the root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a paragraph element
            ParagraphElement paragraph = taggedContent.CreateParagraphElement();

            // Set a custom tag name to represent specialized content
            paragraph.SetTag("MyCustomTag");

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph uses a custom tag for specialized content.");

            // Append the paragraph to the root of the structure tree
            root.AppendChild(paragraph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
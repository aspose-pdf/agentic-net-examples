using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "custom_tagged.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document language and title
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Custom Paragraph Tag");

            // Get the root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Assign a custom tag name to represent specialized content
            paragraph.SetTag("SpecialParagraph");

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph uses a custom tag for specialized content.");

            // Append the paragraph to the root element
            root.AppendChild(paragraph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
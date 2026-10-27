using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "paragraph_title.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Access the tagged content interface to enable accessibility features
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");                     // Set document language
            tagged.SetTitle("Document with Paragraph Title"); // Set document title

            // The root of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a paragraph structure element via the ITaggedContent factory
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph has a title attribute for accessibility."); // Set visible text

            // Set the Title property on the paragraph to provide a concise summary
            paragraph.Title = "Summary of paragraph content";

            // Attach the paragraph to the root element
            root.AppendChild(paragraph);

            // Save the PDF; no PreSave() call is required
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
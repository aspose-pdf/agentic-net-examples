using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Annotations; // GoToAction and XYZExplicitDestination live here

class Program
{
    static void Main()
    {
        const string outputPath = "TaggedZoomed.pdf";

        // Create a new PDF document and ensure deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a single blank page.
            Page page = doc.Pages.Add();

            // Access the tagging API.
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optional: set language for the tagged PDF.
            taggedContent.SetLanguage("en-US");

            // Get the root of the structure tree.
            StructureElement root = taggedContent.RootElement;

            // Create a Span element, set its text, and attach it to the root.
            SpanElement span = taggedContent.CreateSpanElement();
            span.SetText("This is a tagged span.");
            root.AppendChild(span);

            // Set the document's OpenAction to zoom to 130% on the first page.
            // XYZExplicitDestination(page, left, top, zoom)
            doc.OpenAction = new GoToAction(new XYZExplicitDestination(page, 0, 0, 1.3));

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

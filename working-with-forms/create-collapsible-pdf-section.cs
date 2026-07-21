using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "collapsible.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to host the section content
            Page page = doc.Pages.Add();

            // Simple text to illustrate the section
            TextFragment tf = new TextFragment("Section content goes here.");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // Create an outline (bookmark) representing the collapsible section
            OutlineItemCollection outline = new OutlineItemCollection(doc.Outlines)
            {
                Title = "My Section",
                Open = false // start collapsed
            };
            doc.Outlines.Add(outline);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
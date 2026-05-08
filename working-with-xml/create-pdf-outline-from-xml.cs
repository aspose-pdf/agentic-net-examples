using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // Added for XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string xmlPath = "sections.xml";      // input XML file with section titles
        const string outputPdf = "document_with_outline.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and extract titles (assumes elements <section title="...">)
        XDocument xdoc = XDocument.Load(xmlPath);
        var titles = xdoc.Descendants("section")
                         .Attributes("title")
                         .Select(a => a.Value)
                         .ToList();

        if (titles.Count == 0)
        {
            Console.Error.WriteLine("No section titles found in XML.");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Ensure the document has an outline collection
            OutlineCollection outlines = doc.Outlines;

            // Iterate over titles, create a page per title and a corresponding outline entry
            for (int i = 0; i < titles.Count; i++)
            {
                string title = titles[i];

                // Add a new page
                Page page = doc.Pages.Add();

                // Add the title as visible text on the page
                TextFragment tf = new TextFragment(title);
                tf.TextState.FontSize = 24;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                page.Paragraphs.Add(tf);

                // Create an outline item linked to this page
                OutlineItemCollection outlineItem = new OutlineItemCollection(outlines);
                outlineItem.Title = title;
                // Destination: top‑left of the page, zoom 1.0
                outlineItem.Destination = new XYZExplicitDestination(page, 0, page.PageInfo.Height, 1);
                outlineItem.Open = true; // expanded in the outline pane

                // Add the outline item to the document outline hierarchy
                outlines.Add(outlineItem);
            }

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with custom outlines saved to '{outputPdf}'.");
    }
}

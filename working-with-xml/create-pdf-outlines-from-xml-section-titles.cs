using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for destination classes
using Aspose.Pdf.Text;       // for TextFragment

class Program
{
    static void Main()
    {
        const string xmlPath   = "sections.xml";   // input XML containing section titles
        const string pdfPath   = "output.pdf";     // output PDF with outlines

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and extract all section titles (assumes <section title="..."/> elements)
        XDocument xdoc = XDocument.Load(xmlPath);
        var titles = xdoc.Descendants("section")
                         .Select(e => (string)e.Attribute("title"))
                         .Where(t => !string.IsNullOrWhiteSpace(t));

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Iterate over titles, create a page for each, and add an outline entry
            foreach (string title in titles)
            {
                // Add a new page
                Page page = doc.Pages.Add();

                // Add the title text to the page
                TextFragment tf = new TextFragment(title)
                {
                    // Simple formatting – larger bold font for visibility
                    TextState = { FontSize = 24, FontStyle = FontStyles.Bold }
                };
                page.Paragraphs.Add(tf);

                // Create an outline item linked to this page
                OutlineItemCollection outlineItem = new OutlineItemCollection(doc.Outlines)
                {
                    Title = title,
                    // Destination points to the top-left of the page with 100% zoom
                    Destination = new XYZExplicitDestination(page, 0, 0, 1)
                };

                // Add the outline item to the document's outline collection
                doc.Outlines.Add(outlineItem);
            }

            // Save the PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with outlines saved to '{pdfPath}'.");
    }
}
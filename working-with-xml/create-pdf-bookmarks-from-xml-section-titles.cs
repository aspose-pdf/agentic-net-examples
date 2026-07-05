using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // XYZExplicitDestination resides in Aspose.Pdf namespace, but keeping for clarity

class Program
{
    static void Main()
    {
        const string xmlPath = "structure.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document containing section titles.
        XDocument xdoc = XDocument.Load(xmlPath);
        // Expect elements like <section title="Chapter 1"/>
        var sections = xdoc.Descendants("section");

        // Create a new PDF document.
        using (Document pdf = new Document())
        {
            int pageIndex = 0;

            foreach (var sec in sections)
            {
                pageIndex++;

                // Add a new page for the section.
                Page page = pdf.Pages.Add();

                // Write the section title on the page (optional visual aid).
                string title = sec.Attribute("title")?.Value ?? $"Section {pageIndex}";
                TextFragment tf = new TextFragment(title);
                tf.Position = new Position(50, page.PageInfo.Height - 50);
                page.Paragraphs.Add(tf);

                // Create an outline (bookmark) entry pointing to this page.
                // OutlineItemCollection represents a single outline entry.
                OutlineItemCollection outlineItem = new OutlineItemCollection(pdf.Outlines)
                {
                    Title = title,
                    // Destination points to the top of the page (zoom 1.0).
                    Destination = new XYZExplicitDestination(page, 0, page.PageInfo.Height, 1)
                };
                pdf.Outlines.Add(outlineItem);
            }

            // Save the PDF with the generated outline hierarchy.
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF with outlines saved to '{outputPdf}'.");
    }
}

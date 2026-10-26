using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // for XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string xmlPath = "sections.xml";   // input XML with <section><title>...</title></section>
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and extract all section titles
        XDocument xDoc = XDocument.Load(xmlPath);
        var titles = xDoc.Root?
                         .Descendants("section")
                         .Select(s => (string)s.Element("title"))
                         .Where(t => !string.IsNullOrWhiteSpace(t));

        if (titles == null || !titles.Any())
        {
            Console.Error.WriteLine("No <section><title> elements found in the XML.");
            return;
        }

        // Create PDF document and add pages + outline entries
        using (Document pdfDoc = new Document())
        {
            // Root outline collection
            OutlineCollection outlineRoot = pdfDoc.Outlines;

            foreach (string title in titles)
            {
                // Add a new page for the section
                Page page = pdfDoc.Pages.Add();

                // Write the title on the page
                TextFragment tf = new TextFragment(title)
                {
                    TextState = { FontSize = 24, ForegroundColor = Aspose.Pdf.Color.Black }
                };
                page.Paragraphs.Add(tf);

                // Create an outline (bookmark) that points to this page
                // First, create the outline item and set its title
                OutlineItemCollection entry = new OutlineItemCollection(outlineRoot)
                {
                    Title = title
                };
                // Destination: top‑left of the page (coordinates 0,0) with default zoom (1.0)
                entry.Destination = new XYZExplicitDestination(page.Number, 0, 0, 1.0f);
                // Add the entry to the root collection
                outlineRoot.Add(entry);
            }

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with custom outline saved to '{pdfPath}'.");
    }
}

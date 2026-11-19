using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string xmlPath = "sections.xml";
        const string outputPdf = "document_with_outline.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and extract section titles (assumes <section><title>...</title></section>)
        XDocument xDoc = XDocument.Load(xmlPath);
        var titles = xDoc.Root?
                        .Descendants("section")
                        .Select(s => (string)s.Element("title"))
                        .Where(t => !string.IsNullOrWhiteSpace(t))
                        .ToList();

        if (titles == null || titles.Count == 0)
        {
            Console.Error.WriteLine("No section titles found in XML.");
            return;
        }

        // Create a new PDF document and add outline entries
        using (Document pdfDoc = new Document())
        {
            // Ensure the document has at least one page (Aspose.Pdf requires a page to exist before adding outlines)
            pdfDoc.Pages.Add();

            // Root outline collection (type OutlineCollection, not OutlineItemCollection)
            OutlineCollection rootOutline = pdfDoc.Outlines;

            // Iterate over titles, add a page per title and a corresponding bookmark
            for (int i = 0; i < titles.Count; i++)
            {
                string title = titles[i];

                // Add a new page for this section (skip first page if already added)
                Page page = i == 0 ? pdfDoc.Pages[1] : pdfDoc.Pages.Add();

                // Optional: add visible text showing the section title on the page
                TextFragment tf = new TextFragment(title)
                {
                    TextState = { FontSize = 24, FontStyle = FontStyles.Bold }
                };
                page.Paragraphs.Add(tf);

                // Create a bookmark (outline item) that points to this page
                OutlineItemCollection bookmark = new OutlineItemCollection(pdfDoc.Outlines)
                {
                    Title = title,
                    Destination = new XYZExplicitDestination(page, 0, page.PageInfo.Height, 1)
                };

                // Add the bookmark as a top‑level entry
                rootOutline.Add(bookmark);
            }

            // Save the PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with outline saved to '{outputPdf}'.");
    }
}

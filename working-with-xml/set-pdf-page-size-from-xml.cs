using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "pagesize.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML that defines page sizes.
        // Expected format:
        // <Pages>
        //   <Page index="1" width="595" height="842" />
        //   <Page index="2" width="420" height="595" />
        //   ...
        // </Pages>
        XDocument xdoc = XDocument.Load(xmlPath);

        // Create a new PDF document. The using block ensures proper disposal.
        using (Document pdfDoc = new Document())
        {
            // Iterate over each <Page> element in the XML.
            foreach (var pageElem in xdoc.Root.Elements("Page"))
            {
                // Read attributes.
                int index = (int)pageElem.Attribute("index");
                double width = (double)pageElem.Attribute("width");
                double height = (double)pageElem.Attribute("height");

                // Ensure the document has enough pages.
                while (pdfDoc.Pages.Count < index)
                {
                    pdfDoc.Pages.Add();
                }

                // Get the target page (1‑based indexing).
                Page page = pdfDoc.Pages[index];

                // Set the page size using the SetPageSize method.
                page.SetPageSize(width, height);
            }

            // Save the resulting PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
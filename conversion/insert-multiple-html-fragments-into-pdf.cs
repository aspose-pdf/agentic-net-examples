using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the input XML and the output PDF
        const string xmlPath = "fragments.xml";
        const string outputPdf = "output.pdf";

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document that contains the HTML fragments
        XDocument xdoc = XDocument.Load(xmlPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Ensure there is at least one page to start with
            pdfDoc.Pages.Add();

            // Iterate over each <Fragment> element in the XML
            foreach (XElement fragElem in xdoc.Root.Elements("Fragment"))
            {
                // ----- Validate and read required attributes -----
                XAttribute pageAttr = fragElem.Attribute("page");
                XAttribute xAttr = fragElem.Attribute("x");
                XAttribute yAttr = fragElem.Attribute("y");

                if (pageAttr == null || xAttr == null || yAttr == null)
                {
                    Console.Error.WriteLine("Fragment element is missing required attributes (page, x, y). Skipping.");
                    continue;
                }

                int pageNumber = (int)pageAttr;
                double x = (double)xAttr;
                double y = (double)yAttr;
                string html = fragElem.Value.Trim();

                // Add pages as needed so the target page exists
                while (pdfDoc.Pages.Count < pageNumber)
                {
                    pdfDoc.Pages.Add();
                }

                // Get the target page (Aspose.Pdf uses 1‑based indexing)
                Page page = pdfDoc.Pages[pageNumber];

                // Create an HtmlFragment with the supplied HTML markup
                HtmlFragment htmlFragment = new HtmlFragment(html);

                // Position the fragment using margins (Aspose.Pdf does not expose a Position property on HtmlFragment)
                htmlFragment.IsInLineParagraph = false; // make it behave like an absolute positioned element
                htmlFragment.HorizontalAlignment = HorizontalAlignment.Left;
                htmlFragment.VerticalAlignment = VerticalAlignment.Top;
                htmlFragment.Margin = new MarginInfo { Left = x, Top = y };

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(htmlFragment);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPdf}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        const string xmlPath = "pagesizes.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML that defines page sizes.
        XDocument xDoc = XDocument.Load(xmlPath);

        // Create a new PDF document.
        using (Document pdfDoc = new Document())
        {
            // Iterate over each <Page> element in the XML.
            // Expected XML format:
            // <Pages>
            //   <Page index="1">
            //     <Width>595</Width>
            //     <Height>842</Height>
            //   </Page>
            //   ...
            // </Pages>
            foreach (XElement pageElem in xDoc.Root.Elements("Page"))
            {
                // Parse width and height (points). Default to A4 if missing.
                double width = (double?)pageElem.Element("Width") ?? 595;   // A4 width
                double height = (double?)pageElem.Element("Height") ?? 842; // A4 height

                // Add a new blank page to the document.
                pdfDoc.Pages.Add();

                // Aspose.Pdf uses 1‑based indexing for pages.
                int pageIndex = pdfDoc.Pages.Count; // the page we just added
                Page page = pdfDoc.Pages[pageIndex];

                // Set custom page dimensions.
                page.PageInfo.Width = width;
                page.PageInfo.Height = height;
            }

            // Save the resulting PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created with custom page sizes: {outputPdf}");
    }
}
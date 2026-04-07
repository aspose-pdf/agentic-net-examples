using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath   = "layout.xml";   // XML defining page orientations
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML layout definition
        XDocument layoutDoc = XDocument.Load(xmlPath);

        // Create a new empty PDF document
        using (Document pdfDoc = new Document())
        {
            // Example: add blank pages equal to the number of <Page> elements in the XML
            foreach (XElement pageElem in layoutDoc.Root.Elements("Page"))
            {
                pdfDoc.Pages.Add(); // adds a new blank page
            }

            // Apply orientation based on the XML attribute "orientation"
            foreach (XElement pageElem in layoutDoc.Root.Elements("Page"))
            {
                // Expected attributes: number (1‑based) and orientation ("landscape" or "portrait")
                int pageNumber = (int)pageElem.Attribute("number");
                string orientation = (string)pageElem.Attribute("orientation");

                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} in XML.");
                    continue;
                }

                // Set the page orientation
                PageInfo info = pdfDoc.Pages[pageNumber].PageInfo;
                info.IsLandscape = string.Equals(orientation, "landscape", StringComparison.OrdinalIgnoreCase);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML defining page durations and output PDF paths
        const string xmlPath = "pageDurations.xml";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file (using the provided XmlLoadOptions rule)
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Create a new PDF document from the XML (uses the provided load rule)
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Iterate over each <Page> element in the XML
            foreach (XElement pageElem in xmlDoc.Descendants("Page"))
            {
                // Expect attributes: number (1‑based) and duration (seconds)
                XAttribute numberAttr = pageElem.Attribute("number");
                XAttribute durationAttr = pageElem.Attribute("duration");

                if (numberAttr == null || durationAttr == null)
                    continue; // skip malformed entries

                if (!int.TryParse(numberAttr.Value, out int pageNumber))
                    continue; // invalid page number

                if (!double.TryParse(durationAttr.Value, out double durationSeconds))
                    continue; // invalid duration

                // Ensure the page number exists in the document (Aspose.Pdf uses 1‑based indexing)
                if (pageNumber >= 1 && pageNumber <= pdfDoc.Pages.Count)
                {
                    // Set the page display duration (uses the Page.Duration property)
                    pdfDoc.Pages[pageNumber].Duration = durationSeconds;
                }
            }

            // Save the modified PDF (uses the provided save rule)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with page durations to '{outputPdfPath}'.");
    }
}
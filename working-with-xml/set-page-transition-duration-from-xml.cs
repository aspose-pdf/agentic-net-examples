using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths to the source XML and the resulting PDF
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        // Ensure the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML and create a PDF document from it
        // XmlLoadOptions is required for XML → PDF conversion
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Parse the same XML to obtain timing values per page
            // Expected XML format (example):
            // <Document>
            //   <Page number="1" duration="5"/>
            //   <Page number="2" duration="3.5"/>
            //   ...
            // </Document>
            XDocument xDoc = XDocument.Load(xmlPath);
            foreach (XElement pageElem in xDoc.Descendants("Page"))
            {
                // Read the page number (1‑based) and duration (seconds)
                XAttribute numAttr = pageElem.Attribute("number");
                XAttribute durAttr = pageElem.Attribute("duration");
                if (numAttr == null || durAttr == null) continue;

                if (!int.TryParse(numAttr.Value, out int pageNumber)) continue;
                if (!double.TryParse(durAttr.Value, out double duration)) continue;

                // Validate page index against the PDF page collection (1‑based)
                if (pageNumber >= 1 && pageNumber <= pdfDoc.Pages.Count)
                {
                    // Set the display duration for the page
                    pdfDoc.Pages[pageNumber].Duration = duration;
                }
            }

            // Save the modified PDF document
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with page transitions saved to '{pdfPath}'.");
    }
}
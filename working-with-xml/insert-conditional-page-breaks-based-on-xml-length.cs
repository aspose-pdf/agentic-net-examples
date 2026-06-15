using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";
        const int lengthThreshold = 5000; // characters per page before inserting a break

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML content into a PDF document using XmlLoadOptions
        XmlLoadOptions xmlLoad = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, xmlLoad))
        {
            // Parse the XML to evaluate content length
            XDocument xdoc = XDocument.Load(xmlPath);
            int accumulatedLength = 0;

            // Iterate over top‑level elements (adjust as needed for your XML structure)
            foreach (var element in xdoc.Root.Elements())
            {
                // Accumulate the length of the element's textual content
                accumulatedLength += element.Value?.Length ?? 0;

                // When the threshold is exceeded, insert a new empty page
                if (accumulatedLength > lengthThreshold)
                {
                    // Insert after the current last page (Pages are 1‑based)
                    pdfDoc.Pages.Insert(pdfDoc.Pages.Count + 1);
                    // Reset the counter for the next segment
                    accumulatedLength = 0;
                }
            }

            // Save the resulting PDF with the inserted page breaks
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with conditional page breaks saved to '{outputPdf}'.");
    }
}
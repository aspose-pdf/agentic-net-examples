using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Aspose.Pdf;

public class SetPageDurationFromXml
{
    public static void Main()
    {
        // Create a sample PDF with three pages
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Save("sample.pdf");
        }

        // Create an XML file defining durations for each page (in seconds)
        XDocument xmlDoc = new XDocument(
            new XElement("Durations",
                new XElement("Page", new XAttribute("number", 1), new XAttribute("duration", 3)),
                new XElement("Page", new XAttribute("number", 2), new XAttribute("duration", 5)),
                new XElement("Page", new XAttribute("number", 3), new XAttribute("duration", 7))
            )
        );
        xmlDoc.Save("durations.xml");

        // Load the XML file
        XDocument loadedXml = XDocument.Load("durations.xml");

        // Load the PDF and set page durations
        using (Document pdfDoc = new Document("sample.pdf"))
        {
            foreach (XElement pageElement in loadedXml.Root.Elements("Page"))
            {
                int pageNumber = (int)pageElement.Attribute("number");
                double duration = (double)pageElement.Attribute("duration");
                if (pageNumber >= 1 && pageNumber <= pdfDoc.Pages.Count)
                {
                    pdfDoc.Pages[pageNumber].Duration = duration;
                }
            }

            // Save the updated PDF
            pdfDoc.Save("output.pdf");
        }
    }
}

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string xmlPath = "pageDurations.xml"; // XML with timing values
        const string outputPath = "output.pdf";      // result PDF

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load timing values from XML.
        // Expected format:
        // <Pages>
        //   <Page number="1" duration="5.0" />
        //   <Page number="2" duration="3.5" />
        //   ...
        // </Pages>
        XDocument xmlDoc = XDocument.Load(xmlPath);
        var durationMap = new System.Collections.Generic.Dictionary<int, double>();
        foreach (var pageElem in xmlDoc.Root.Elements("Page"))
        {
            int number = (int)pageElem.Attribute("number");
            double duration = (double)pageElem.Attribute("duration");
            durationMap[number] = duration;
        }

        // Open the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over pages (1‑based indexing).
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                if (durationMap.TryGetValue(i, out double dur))
                {
                    // Set the display duration (in seconds) for the page.
                    pdfDoc.Pages[i].Duration = dur;
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page durations to '{outputPath}'.");
    }
}
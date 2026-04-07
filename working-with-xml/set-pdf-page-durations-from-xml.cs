using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string xmlPath   = "timings.xml";
        const string outputPdf = "output.pdf";

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

        // Load timing definitions from XML.
        // Expected format:
        // <Timings>
        //   <Page number="1" duration="5.0" />
        //   <Page number="2" duration="3.5" />
        //   ...
        // </Timings>
        XDocument xDoc = XDocument.Load(xmlPath);
        var timingMap = new System.Collections.Generic.Dictionary<int, double>();

        foreach (var elem in xDoc.Root.Elements("Page"))
        {
            int number;
            double duration;
            if (int.TryParse((string)elem.Attribute("number"), out number) &&
                double.TryParse((string)elem.Attribute("duration"), out duration))
            {
                timingMap[number] = duration;
            }
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over all pages (1‑based indexing).
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // If a duration is defined for this page, set it.
                if (timingMap.TryGetValue(i, out double dur))
                {
                    page.Duration = dur; // duration is in seconds
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Page durations applied and saved to '{outputPdf}'.");
    }
}
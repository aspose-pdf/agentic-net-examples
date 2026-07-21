using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfInputPath  = "input.pdf";
        const string xmlTimingPath = "timings.xml";
        const string pdfOutputPath = "output_with_durations.pdf";

        // Verify files exist
        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(xmlTimingPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlTimingPath}");
            return;
        }

        // Load timing information from XML.
        // Expected format:
        // <Pages>
        //   <Page number="1" duration="5.0" />
        //   <Page number="2" duration="3.5" />
        //   ...
        // </Pages>
        XDocument xmlDoc = XDocument.Load(xmlTimingPath);
        var pageDurations = xmlDoc.Root?.Elements("Page");

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfInputPath))
        {
            if (pageDurations != null)
            {
                foreach (var pageElem in pageDurations)
                {
                    // Parse page number (1‑based) and duration (seconds).
                    if (int.TryParse(pageElem.Attribute("number")?.Value, out int pageNumber) &&
                        double.TryParse(pageElem.Attribute("duration")?.Value, out double duration))
                    {
                        // Ensure the page exists.
                        if (pageNumber >= 1 && pageNumber <= pdfDoc.Pages.Count)
                        {
                            pdfDoc.Pages[pageNumber].Duration = duration;
                        }
                        else
                        {
                            Console.Error.WriteLine($"Page {pageNumber} is out of range.");
                        }
                    }
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF saved with page durations to '{pdfOutputPath}'.");
    }
}
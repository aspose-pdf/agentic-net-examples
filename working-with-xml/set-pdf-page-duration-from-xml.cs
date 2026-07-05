using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "input.pdf";
        const string xmlConfigPath = "pageDurations.xml";
        const string outputPdfPath = "output.pdf";

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlConfigPath))
        {
            Console.Error.WriteLine($"XML config not found: {xmlConfigPath}");
            return;
        }

        // Load timing values from XML.
        // Expected format:
        // <Pages>
        //   <Page number="1" duration="5.0" />
        //   <Page number="2" duration="3.5" />
        //   ...
        // </Pages>
        var durations = new System.Collections.Generic.Dictionary<int, double>();
        try
        {
            XDocument doc = XDocument.Load(xmlConfigPath);
            foreach (var pageElem in doc.Root.Elements("Page"))
            {
                int number = (int)pageElem.Attribute("number");
                double duration = (double)pageElem.Attribute("duration");
                durations[number] = duration;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse XML: {ex.Message}");
            return;
        }

        // Open PDF, set page durations, and save.
        try
        {
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Ensure we don't exceed page count
                int pageCount = pdfDoc.Pages.Count;
                for (int i = 1; i <= pageCount; i++)
                {
                    if (durations.TryGetValue(i, out double dur))
                    {
                        // Page.Duration is a double representing seconds.
                        pdfDoc.Pages[i].Duration = dur;
                    }
                }

                // Save the modified PDF.
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Page durations applied and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string layoutXmlPath = "layout.xml";     // XML with orientation info
        const string outputPdfPath = "output.pdf";     // result PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(layoutXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {layoutXmlPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Load the XML layout definition
                XDocument layoutDoc = XDocument.Load(layoutXmlPath);

                // Iterate over each <Page> element in the XML
                foreach (XElement pageElem in layoutDoc.Root.Elements("Page"))
                {
                    // Read the page number (1‑based) and desired orientation
                    int pageNumber = (int?)pageElem.Attribute("number") ?? 0;
                    string orientation = (string)pageElem.Attribute("orientation") ?? "portrait";

                    // Validate page number
                    if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                        continue; // skip invalid page numbers

                    // Set the page orientation based on the XML attribute
                    // PageInfo.IsLandscape = true makes the page landscape; false makes it portrait
                    doc.Pages[pageNumber].PageInfo.IsLandscape = 
                        orientation.Equals("landscape", StringComparison.OrdinalIgnoreCase);
                }

                // Save the modified PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF saved with custom orientations to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
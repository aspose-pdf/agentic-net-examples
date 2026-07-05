using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the XML layout definition and the source PDF
        const string xmlPath = "layout.xml";
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify required files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML layout file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // Load the XML and read the orientation attribute (e.g., <Layout orientation="landscape"/>)
        XDocument layoutDoc = XDocument.Load(xmlPath);
        string orientation = (string)layoutDoc.Root.Attribute("orientation") ?? "portrait";

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Apply orientation to each page based on the XML value
            bool makeLandscape = orientation.Equals("landscape", StringComparison.OrdinalIgnoreCase);
            foreach (Page page in pdfDoc.Pages)
            {
                // PageInfo.IsLandscape controls the page orientation
                page.PageInfo.IsLandscape = makeLandscape;
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}' with {(orientation.Equals("landscape", StringComparison.OrdinalIgnoreCase) ? "landscape" : "portrait")} orientation.");
    }
}
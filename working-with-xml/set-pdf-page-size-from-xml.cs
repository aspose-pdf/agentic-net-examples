using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the XML that defines the page size.
        const string pdfPath   = "source.pdf";
        const string xmlPath   = "pagesize.xml";
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

        // Load the XML and extract width/height values (assumed to be in points).
        double width, height;
        try
        {
            XDocument xDoc = XDocument.Load(xmlPath);
            XElement sizeElement = xDoc.Root; // e.g. <PageSize width="595" height="842"/>
            width  = double.Parse(sizeElement.Attribute("width")?.Value ?? "0");
            height = double.Parse(sizeElement.Attribute("height")?.Value ?? "0");

            if (width <= 0 || height <= 0)
                throw new InvalidDataException("Invalid page size values in XML.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read page size from XML: {ex.Message}");
            return;
        }

        // Open the PDF, adjust each page size, and save the result.
        using (Document doc = new Document(pdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Set the new size (width and height are in points).
                page.SetPageSize(width, height);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Page size set to {width}×{height} points and saved to '{outputPdf}'.");
    }
}
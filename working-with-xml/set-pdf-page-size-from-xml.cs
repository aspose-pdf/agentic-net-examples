using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath   = "pagesize.xml";   // XML defining width and height
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // result PDF

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdf}");
            return;
        }

        // Load page size values from XML (expects attributes width and height)
        double width, height;
        try
        {
            XDocument doc = XDocument.Load(xmlPath);
            XElement sizeElem = doc.Root; // e.g. <PageSize width="595" height="842"/>
            width  = double.Parse(sizeElem.Attribute("width")?.Value  ?? "0");
            height = double.Parse(sizeElem.Attribute("height")?.Value ?? "0");
            if (width <= 0 || height <= 0)
                throw new InvalidDataException("Invalid width/height values in XML.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read XML: {ex.Message}");
            return;
        }

        // Load the PDF, adjust each page size, and save
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Page indexing in Aspose.Pdf is 1‑based
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                page.SetPageSize(width, height); // apply size from XML
            }

            pdfDoc.Save(outputPdf); // save as PDF (no SaveOptions needed)
        }

        Console.WriteLine($"PDF saved with new page size ({width} x {height}) to '{outputPdf}'.");
    }
}
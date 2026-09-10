using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string inputXmlPath  = "pagesize.xml";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {inputXmlPath}");
            return;
        }

        // Load page size values from the XML file.
        // Expected XML format:
        // <PageSize>
        //     <Width>595</Width>
        //     <Height>842</Height>
        // </PageSize>
        XDocument xmlDoc = XDocument.Load(inputXmlPath);
        XElement sizeElem = xmlDoc.Root;
        double width  = double.Parse(sizeElem.Element("Width")?.Value ?? "0");
        double height = double.Parse(sizeElem.Element("Height")?.Value ?? "0");

        if (width <= 0 || height <= 0)
        {
            Console.Error.WriteLine("Invalid page size values in XML.");
            return;
        }

        // Load the PDF, adjust each page size, and save.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                // Set the page size using the SetPageSize method.
                page.SetPageSize(width, height);
            }

            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page size set to {width}x{height} and saved to '{outputPdfPath}'.");
    }
}
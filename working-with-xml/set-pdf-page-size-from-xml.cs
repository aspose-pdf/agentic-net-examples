using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "pagesize.xml";
        const string outputPdf = "output.pdf";

        // Verify that the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML and read width/height values (in points)
        XDocument xdoc = XDocument.Load(xmlPath);
        // Expected XML format:
        // <PageSize>
        //     <Width>595</Width>
        //     <Height>842</Height>
        // </PageSize>
        double width = (double?)xdoc.Root.Element("Width") ?? 595;   // default A4 width
        double height = (double?)xdoc.Root.Element("Height") ?? 842; // default A4 height

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page is at index 1)
            Page page = doc.Pages.Add();

            // Apply the size extracted from the XML
            page.SetPageSize(width, height);

            // Example content (optional)
            // TextFragment tf = new TextFragment($"Page size: {width} x {height}");
            // page.Paragraphs.Add(tf);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created with page size {width}x{height} points at '{outputPdf}'.");
    }
}
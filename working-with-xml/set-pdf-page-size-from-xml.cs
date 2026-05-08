using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the XML definition and the PDF files
        const string xmlPath = "pagesize.xml";
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdfPath}");
            return;
        }

        // Parse the XML to obtain width and height (in points)
        double width = 0, height = 0;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);
        XmlNode sizeNode = xmlDoc.SelectSingleNode("//PageSize");
        if (sizeNode == null ||
            !double.TryParse(sizeNode.Attributes["width"]?.Value, out width) ||
            !double.TryParse(sizeNode.Attributes["height"]?.Value, out height))
        {
            Console.Error.WriteLine("Invalid XML format. Expected <PageSize width=\"...\" height=\"...\"/>");
            return;
        }

        // Load the PDF, set each page's size, and save the result
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            foreach (Page page in pdfDoc.Pages)
            {
                // Set the page size using the values from the XML
                page.SetPageSize(width, height);
            }

            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with new page size {width}x{height} to '{outputPdfPath}'.");
    }
}
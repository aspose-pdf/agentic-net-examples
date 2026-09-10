using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";          // XML containing <svg> elements
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Find all <svg> nodes (ignoring namespaces for simplicity)
            XmlNodeList svgNodes = xmlDoc.GetElementsByTagName("svg");
            if (svgNodes.Count == 0)
            {
                Console.WriteLine("No SVG elements found in the XML.");
            }

            int pageNumber = 1;
            foreach (XmlNode svgNode in svgNodes)
            {
                // Create a new page for each SVG
                Page page = pdfDoc.Pages.Add();

                // Convert the SVG node back to its XML string
                string svgContent = svgNode.OuterXml;
                byte[] svgBytes = System.Text.Encoding.UTF8.GetBytes(svgContent);

                // Use a memory stream so we don't need temporary files
                using (MemoryStream svgStream = new MemoryStream(svgBytes))
                {
                    // Define the rectangle where the SVG will be placed.
                    // Adjust the size as needed; here we use the page size.
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                        page.PageInfo.Margin.Left,
                        page.PageInfo.Margin.Bottom,
                        page.PageInfo.Width - page.PageInfo.Margin.Right,
                        page.PageInfo.Height - page.PageInfo.Margin.Top);

                    // Add the SVG as an image. Aspose.Pdf detects the SVG format
                    // and renders it as vector graphics.
                    page.AddImage(svgStream, rect);
                }

                Console.WriteLine($"Embedded SVG #{pageNumber} on page {pageNumber}.");
                pageNumber++;
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded SVGs saved to '{outputPdf}'.");
    }
}